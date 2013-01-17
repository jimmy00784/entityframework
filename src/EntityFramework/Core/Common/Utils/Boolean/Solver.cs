﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Common.Utils.Boolean
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using IfThenElseKey = Triple<Vertex, Vertex, Vertex>;

    /// <summary>
    /// Supports construction of canonical Boolean expressions as Reduced Ordered
    /// Boolean Decision Diagrams (ROBDD). As a side effect, supports simplification and SAT:
    /// 
    /// - The canonical form of a valid expression is Solver.One
    /// - The canonical form of an unsatisfiable expression is Solver.Zero
    /// - The lack of redundancy in the trees allows us to produce compact representations
    /// of expressions
    /// 
    /// Any method taking a Vertex argument requires that the argument is either
    /// a 'sink' (Solver.One or Solver.Zero) or generated by this Solver instance.
    /// </summary>
    internal sealed class Solver
    {
        #region Fields

        private readonly Dictionary<IfThenElseKey, Vertex> _computedIfThenElseValues =
            new Dictionary<IfThenElseKey, Vertex>();

        private readonly Dictionary<Vertex, Vertex> _knownVertices =
            new Dictionary<Vertex, Vertex>(VertexValueComparer.Instance);

        private int _variableCount;
        // a standard Boolean variable has children '1' and '0'
        internal static readonly Vertex[] BooleanVariableChildren = new[] { Vertex.One, Vertex.Zero };

        #endregion

        #region Expression factory methods

        internal int CreateVariable()
        {
            return ++_variableCount;
        }

        internal Vertex Not(Vertex vertex)
        {
            // Not(v) iff. 'if v then 0 else 1'
            return IfThenElse(vertex, Vertex.Zero, Vertex.One);
        }

        internal Vertex And(IEnumerable<Vertex> children)
        {
            // assuming input vertices v1, v2, ..., vn:
            //
            //  v1
            //  0|\1
            //   |  v2
            //   |/0  \1
            //   |    ...
            //   |  /0  \1
            //   |        vn
            //   |     /0   \1
            //   FALSE       TRUE
            //
            // order the children to minimize churn when building tree bottom up
            return children
                .OrderByDescending(child => child.Variable)
                .Aggregate(Vertex.One, (left, right) => IfThenElse(left, right, Vertex.Zero));
        }

        internal Vertex And(Vertex left, Vertex right)
        {
            // left AND right iff. if 'left' then 'right' else '0'
            return IfThenElse(left, right, Vertex.Zero);
        }

        internal Vertex Or(IEnumerable<Vertex> children)
        {
            // assuming input vertices v1, v2, ..., vn:
            //
            //  v1
            //  1|\0
            //   |  v2
            //   |/1  \0
            //   |    ...
            //   |  /1  \0
            //   |        vn
            //   |     /1   \0
            //   TRUE       FALSE
            //
            // order the children to minimize churn when building tree bottom up
            return children
                .OrderByDescending(child => child.Variable)
                .Aggregate(Vertex.Zero, (left, right) => IfThenElse(left, Vertex.One, right));
        }

        /// <summary>
        /// Creates a leaf vertex; all children must be sinks
        /// </summary>
        internal Vertex CreateLeafVertex(int variable, Vertex[] children)
        {
            Debug.Assert(null != children, "children must be specified");
            Debug.Assert(2 <= children.Length, "must be at least 2 children");
            Debug.Assert(children.All(child => child != null), "children must not be null");
            Debug.Assert(children.All(child => child.IsSink()), "children must be sinks");
            Debug.Assert(variable <= _variableCount, "variable out of range");

            return GetUniqueVertex(variable, children);
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Returns a Vertex with the given configuration. If this configuration
        /// is known, returns the existing vertex. Otherwise, a new
        /// vertex is created. This ensures the vertex is unique in the context
        /// of this solver.
        /// </summary>
        private Vertex GetUniqueVertex(int variable, Vertex[] children)
        {
            AssertVerticesValid(children);

            var result = new Vertex(variable, children);

            // see if we know this vertex already
            Vertex canonicalResult;
            if (_knownVertices.TryGetValue(result, out canonicalResult))
            {
                return canonicalResult;
            }

            // remember the vertex (because it came first, it's canonical)
            _knownVertices.Add(result, result);

            return result;
        }

        /// <summary>
        /// Composes the given vertices to produce a new ROBDD.
        /// </summary>
        private Vertex IfThenElse(Vertex condition, Vertex then, Vertex @else)
        {
            AssertVertexValid(condition);
            AssertVertexValid(then);
            AssertVertexValid(@else);

            // check for terminal conditions in the recursion
            if (condition.IsOne())
            {
                // if '1' then 'then' else '@else' iff. 'then'
                return then;
            }
            if (condition.IsZero())
            {
                // if '0' then 'then' else '@else' iff. '@else'
                return @else;
            }
            if (then.IsOne()
                && @else.IsZero())
            {
                // if 'condition' then '1' else '0' iff. condition
                return condition;
            }
            if (then.Equals(@else))
            {
                // if 'condition' then 'x' else 'x' iff. x
                return then;
            }

            Vertex result;
            var key = new IfThenElseKey(condition, then, @else);

            // check if we've already computed this result
            if (_computedIfThenElseValues.TryGetValue(key, out result))
            {
                return result;
            }

            int topVariableDomainCount;
            var topVariable = DetermineTopVariable(condition, then, @else, out topVariableDomainCount);

            // Recursively compute the new BDD node
            // Note that we preserve the 'ordered' invariant since the child nodes
            // cannot contain references to variables < topVariable, and
            // the topVariable is eliminated from the children through 
            // the call to EvaluateFor.
            var resultCases = new Vertex[topVariableDomainCount];
            var allResultsEqual = true;
            for (var i = 0; i < topVariableDomainCount; i++)
            {
                resultCases[i] = IfThenElse(
                    EvaluateFor(condition, topVariable, i),
                    EvaluateFor(then, topVariable, i),
                    EvaluateFor(@else, topVariable, i));

                if (i > 0 && // first vertex is equivalent to itself
                    allResultsEqual
                    && // we've already found a mismatch
                    !resultCases[i].Equals(resultCases[0]))
                {
                    allResultsEqual = false;
                }
            }

            // if the results are identical, any may be returned
            if (allResultsEqual)
            {
                return resultCases[0];
            }

            // create new vertex
            result = GetUniqueVertex(topVariable, resultCases);

            // remember result so that we don't try to compute this if-then-else pattern again
            _computedIfThenElseValues.Add(key, result);

            return result;
        }

        /// <summary>
        /// Given parts of an if-then-else statement, determines the top variable (nearest
        /// root). Used to determine which variable forms the root of a composed Vertex.
        /// </summary>
        private static int DetermineTopVariable(Vertex condition, Vertex then, Vertex @else, out int topVariableDomainCount)
        {
            int topVariable;
            if (condition.Variable
                < then.Variable)
            {
                topVariable = condition.Variable;
                topVariableDomainCount = condition.Children.Length;
            }
            else
            {
                topVariable = then.Variable;
                topVariableDomainCount = then.Children.Length;
            }
            if (@else.Variable < topVariable)
            {
                topVariable = @else.Variable;
                topVariableDomainCount = @else.Children.Length;
            }
            return topVariable;
        }

        /// <summary>
        /// Returns 'vertex' evaluated for the given value of 'variable'. Requires that
        /// the variable is less than or equal to vertex.Variable.
        /// </summary>
        private static Vertex EvaluateFor(Vertex vertex, int variable, int variableAssigment)
        {
            if (variable < vertex.Variable)
            {
                // If the variable we're setting is less than the vertex variable, the
                // the Vertex 'ordered' invariant ensures that the vertex contains no reference
                // to that variable. Binding the variable is therefore a no-op.
                return vertex;
            }
            Debug.Assert(
                variable == vertex.Variable,
                "variable must be less than or equal to vertex.Variable");

            // If the 'vertex' is conditioned on the given 'variable', the children
            // represent the decompositions of the function for various assignments
            // to that variable.
            Debug.Assert(variableAssigment < vertex.Children.Length, "variable assignment out of range");
            return vertex.Children[variableAssigment];
        }

        /// <summary>
        /// Checks requirements for vertices. 
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [Conditional("DEBUG")]
        private void AssertVerticesValid(IEnumerable<Vertex> vertices)
        {
            Debug.Assert(null != vertices);
            foreach (var vertex in vertices)
            {
                AssertVertexValid(vertex);
            }
        }

        /// <summary>
        /// Checks requirements for a vertex argument (must not be null, and must be in scope
        /// for this solver)
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [Conditional("DEBUG")]
        private void AssertVertexValid(Vertex vertex)
        {
            Debug.Assert(vertex != null, "vertex must not be null");

            // sinks are ok
            if (!vertex.IsSink())
            {
                // so are vertices created by this solver
                Vertex comparisonVertex;
                Debug.Assert(
                    _knownVertices.TryGetValue(vertex, out comparisonVertex) &&
                    comparisonVertex.Equals(vertex), "vertex not created by this solver");
            }
        }

        #endregion

        /// <summary>
        /// Supports value comparison of vertices. In general, we use reference comparison
        /// since the Solver ensures a single instance of each canonical Vertex. The Solver
        /// needs this comparer to ensure a single instance of each canonical Vertex though...
        /// </summary>
        private class VertexValueComparer : IEqualityComparer<Vertex>
        {
            private VertexValueComparer()
            {
            }

            internal static readonly VertexValueComparer Instance = new VertexValueComparer();

            public bool Equals(Vertex x, Vertex y)
            {
                if (x.IsSink())
                {
                    // sync nodes '1' and '0' each have one static instance; use reference
                    return x.Equals(y);
                }

                if (x.Variable != y.Variable
                    ||
                    x.Children.Length != y.Children.Length)
                {
                    return false;
                }
                for (var i = 0; i < x.Children.Length; i++)
                {
                    // use reference comparison for the children (they must be
                    // canonical already)
                    if (!x.Children[i].Equals(y.Children[i]))
                    {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(Vertex vertex)
            {
                // sync nodes '1' and '0' each have one static instance; use reference
                if (vertex.IsSink())
                {
                    return vertex.GetHashCode();
                }

                Debug.Assert(2 <= vertex.Children.Length, "internal vertices must have at least 2 children");
                unchecked
                {
                    return ((vertex.Children[0].GetHashCode() << 5) + 1) + vertex.Children[1].GetHashCode();
                }
            }
        }
    }

    /// <summary>
    /// Record structure containing three values.
    /// </summary>
    internal struct Triple<T1, T2, T3> : IEquatable<Triple<T1, T2, T3>>
        where T1 : IEquatable<T1>
        where T2 : IEquatable<T2>
        where T3 : IEquatable<T3>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;

        internal Triple(T1 value1, T2 value2, T3 value3)
        {
            Debug.Assert(null != value1, "null key element");
            Debug.Assert(null != value2, "null key element");
            Debug.Assert(null != value3, "null key element");
            _value1 = value1;
            _value2 = value2;
            _value3 = value3;
        }

        public bool Equals(Triple<T1, T2, T3> other)
        {
            return _value1.Equals(other._value1) &&
                   _value2.Equals(other._value2) &&
                   _value3.Equals(other._value3);
        }

        public override bool Equals(object obj)
        {
            Debug.Fail("used typed Equals");
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _value1.GetHashCode() ^
                   _value2.GetHashCode() ^
                   _value3.GetHashCode();
        }
    }
}

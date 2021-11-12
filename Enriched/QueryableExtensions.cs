using Enriched.EnumerableExtended;
using Enriched.ExpressionExtended;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Enriched.QueryableExtended
{
    public static class QueryableExtensions
    {
        public static IQueryable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
                    this IQueryable<TLeft> left,
                    IQueryable<TRight> right,
                    Expression<Func<TLeft, TKey>> leftKeySelector,
                    Expression<Func<TRight, TKey>> rightKeySelector,
                    Expression<Func<JoinResult<TLeft, TRight>, TResult>> resultSelector)
        {
            IQueryable<TResult> leftOuterJoinResult = left.LeftOuterJoin(right, leftKeySelector, rightKeySelector, resultSelector);
            IQueryable<TResult> rightOuterJoinResult = right
                .LeftOuterJoin(
                    left,
                    rightKeySelector,
                    leftKeySelector,
                    jr => new JoinResult<TLeft, TRight>
                    {
                        Left = jr.Right,
                        Right = jr.Left
                    })
                .Select(resultSelector);

            return leftOuterJoinResult.Union(rightOuterJoinResult);
        }

        public static IQueryable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(
                            this IQueryable<TLeft> left,
                            IEnumerable<TRight> right,
                            Expression<Func<TLeft, TKey>> leftKeySelector,
                            Expression<Func<TRight, TKey>> rightKeySelector,
                            Expression<Func<JoinResult<TLeft, TRight>, TResult>> resultSelector)
        {
            IQueryable<GroupJoinResult<TLeft, TRight>> groupJoinResult = left
                .GroupJoin(
                    right,
                    leftKeySelector,
                    rightKeySelector,
                    (l, r) => new GroupJoinResult<TLeft, TRight>
                    {
                        Key = l,
                        Values = r
                    });

            IQueryable<TResult> leftOuterJoinResults = groupJoinResult
                .SelectMany(
                    x => x.Values.Select(c => new JoinResult<TLeft, TRight>
                    {
                        Left = x.Key,
                        Right = c
                    }))
                .Select(resultSelector);

            return leftOuterJoinResults;
        }

        public static ICollection<T> ToCollection<T>(this IQueryable<T> queryable)
        {
            return queryable.AsEnumerable().ToCollection();
        }

        public static HashSet<T> ToHashSet<T>(this IQueryable<T> queryable)
        {
            return queryable.AsEnumerable().ToHashSet();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IQueryable<T> queryable)
        {
            return queryable.AsEnumerable().ToObservableCollection();
        }

        public static IEnumerable<TEntity> ToPaged<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate)
        {
            return queryable.AsEnumerable().ToReadOnlyCollection();
        }

        public static IQueryable<T> WhereNot<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate)
        {
            return queryable.Where(predicate.Invert());
        }
    }
}
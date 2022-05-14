using System;
using System.Linq.Expressions;

namespace Enriched.ExpressionExtended
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        public static MemberExpression GetMemberExpression<TSource, TProperty>(this Expression<Func<TSource, TProperty>> property)
        {
            if (Equals(property, null))
            {
                throw new NullReferenceException($"{nameof(property)} is required");
            }

            MemberExpression expr;

            if (property.Body is MemberExpression)
            {
                expr = (MemberExpression)property.Body;
            }
            else if (property.Body is UnaryExpression)
            {
                expr = (MemberExpression)((UnaryExpression)property.Body).Operand;
            }
            else
            {
                const string format = "Expression '{0}' not supported.";
                string message = string.Format(format, property);

                throw new ArgumentException(message, "Property");
            }

            return expr;
        }

        public static string GetPropertyName<TSource, TProperty>(this Expression<Func<TSource, TProperty>> property)
        {
            return property.GetMemberExpression().Member.Name;
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters[0]);
        }
        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _newValue;
            private readonly Expression _oldValue;
            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (ReferenceEquals(node, _oldValue))
                    return _newValue;
                return base.Visit(node);
            }
        }
    }
}
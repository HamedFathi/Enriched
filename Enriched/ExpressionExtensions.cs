using System;
using System.Linq;
using System.Linq.Expressions;

namespace Enriched.ExpressionExtended
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<TParam, bool>> AndAlso<TParam>(this Expression<Func<TParam, bool>> funcA, Expression<Func<TParam, bool>> funcB)
        {
            var @params = funcA.Parameters.Zip(funcB.Parameters, (a, b) => (ParamA: (Expression)a, ParamB: (Expression)b)).ToArray();

            return Expression.Lambda<Func<TParam, bool>>(Expression.AndAlso(funcA.Body, Replace(funcB.Body, @params)), funcA.Parameters);
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

            return expr.Member.Name;
        }

        public static Expression<Func<T, bool>> Invert<T>(this Expression<Func<T, bool>> original)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(original.Body), original.Parameters.Single());
        }

        public static Expression<Func<TParam, bool>> OrElse<TParam>(this Expression<Func<TParam, bool>> funcA, Expression<Func<TParam, bool>> funcB)
        {
            var @params = funcA.Parameters.Zip(funcB.Parameters, (a, b) => (ParamA: (Expression)a, ParamB: (Expression)b)).ToArray();

            return Expression.Lambda<Func<TParam, bool>>(Expression.OrElse(funcA.Body, Replace(funcB.Body, @params)), funcA.Parameters);
        }

        private static Expression Replace(Expression expression, (Expression ParamA, Expression ParamB)[] @params)
        {
            if (expression == default)
                return expression;

            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Add(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.AddChecked:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.AddChecked(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.And:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.And(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.AndAlso:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.AndAlso(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.ArrayLength:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.ArrayLength(Replace(exp.Operand, @params));
                    }

                case ExpressionType.ArrayIndex:
                    {
                        var exp = (MethodCallExpression)expression;
                        return Expression.ArrayIndex(Replace(exp.Object, @params), exp.Arguments.Select(p => Replace(p, @params)));
                    }

                case ExpressionType.Assign:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Assign(Replace(exp.Left, @params), Replace(exp.Right, @params));
                    }

                case ExpressionType.Block:
                    {
                        var exp = (BlockExpression)expression;
                        return Expression.Block(exp.Type, exp.Variables, exp.Expressions.Select(e => Replace(e, @params)));
                    }

                case ExpressionType.Call:
                    {
                        var exp = (MethodCallExpression)expression;
                        return Expression.Call(Replace(exp.Object, @params), exp.Method, exp.Arguments.Select(p => Replace(p, @params)));
                    }

                case ExpressionType.Coalesce:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Coalesce(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Conversion);
                    }

                case ExpressionType.Conditional:
                    {
                        var exp = (ConditionalExpression)expression;
                        return Expression.Condition(Replace(exp.Test, @params), Replace(exp.IfTrue, @params), exp.IfFalse, exp.Type);
                    }

                case ExpressionType.Constant:
                    return expression;

                case ExpressionType.Convert:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.Convert(Replace(exp.Operand, @params), exp.Type, exp.Method);
                    }

                case ExpressionType.ConvertChecked:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.ConvertChecked(Replace(exp.Operand, @params), exp.Type, exp.Method);
                    }

                case ExpressionType.DebugInfo:
                    return expression;

                case ExpressionType.Decrement:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.Decrement(Replace(exp.Operand, @params), exp.Method);
                    }

                case ExpressionType.Dynamic:
                    {
                        var exp = (DynamicExpression)expression;
                        return Expression.Dynamic(exp.Binder, exp.Type, exp.Arguments.Select(e => Replace(e, @params)));
                    }

                case ExpressionType.GreaterThanOrEqual:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.GreaterThanOrEqual(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.IsLiftedToNull, exp.Method);
                    }

                case ExpressionType.Divide:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Divide(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.Equal:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Equal(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.IsLiftedToNull, exp.Method);
                    }

                case ExpressionType.ExclusiveOr:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.ExclusiveOr(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.GreaterThan:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.GreaterThan(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.IsLiftedToNull, exp.Method);
                    }

                case ExpressionType.Invoke:
                    {
                        var exp = (InvocationExpression)expression;
                        return Expression.Invoke(Replace(exp.Expression, @params), exp.Arguments.Select(arg => Replace(arg, @params)));
                    }

                case ExpressionType.Lambda:
                    {
                        var exp = (LambdaExpression)expression;
                        return Expression.Lambda(exp.Type, Replace(exp.Body, @params), exp.Name, exp.TailCall, exp.Parameters);
                    }

                case ExpressionType.LeftShift:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.LeftShift(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.LessThan:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.LessThan(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.IsLiftedToNull, exp.Method);
                    }

                case ExpressionType.LessThanOrEqual:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.LessThanOrEqual(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.IsLiftedToNull, exp.Method);
                    }

                case ExpressionType.ListInit:
                    {
                        var exp = (ListInitExpression)expression;
                        return exp.Update(exp.NewExpression, exp.Initializers.Select(i => Replace(i, @params)));
                    }

                case ExpressionType.MemberAccess:
                    {
                        var exp = (MemberExpression)expression;

                        if (exp.Expression == default)
                        {
                            return exp;
                        }

                        return Expression.PropertyOrField(Replace(exp.Expression, @params), exp.Member.Name);
                    }

                case ExpressionType.MemberInit:
                    {
                        var exp = (MemberInitExpression)expression;
                        return Expression.MemberInit(exp.NewExpression, exp.Bindings.Select(m => Replace(m, @params)));
                    }

                case ExpressionType.Modulo:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Modulo(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.Multiply:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Multiply(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.MultiplyChecked:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.MultiplyChecked(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.Negate:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.Negate(Replace(exp.Operand, @params), exp.Method);
                    }

                case ExpressionType.NegateChecked:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.NegateChecked(Replace(exp.Operand, @params), exp.Method);
                    }

                case ExpressionType.New:
                    {
                        var exp = (NewExpression)expression;
                        return exp.Members != null
                                ? Expression.New(exp.Constructor, exp.Arguments.Select(a => Replace(a, @params)), exp.Members)
                                : Expression.New(exp.Constructor, exp.Arguments.Select(a => Replace(a, @params)));
                    }

                case ExpressionType.NewArrayBounds:
                    {
                        var exp = (NewArrayExpression)expression;
                        return Expression.NewArrayBounds(exp.Type, exp.Expressions.Select(e => Replace(e, @params)));
                    }

                case ExpressionType.NewArrayInit:
                    {
                        var exp = (NewArrayExpression)expression;
                        return Expression.NewArrayInit(exp.Type, exp.Expressions.Select(e => Replace(e, @params)));
                    }

                case ExpressionType.Not:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.Not(Replace(exp.Operand, @params), exp.Method);
                    }

                case ExpressionType.NotEqual:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.NotEqual(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.IsLiftedToNull, exp.Method);
                    }

                case ExpressionType.Or:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Or(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.OrElse:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.OrElse(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.Parameter:
                    {
                        foreach (var pair in @params)
                        {
                            if (expression == pair.ParamB)
                                return pair.ParamA;
                        }

                        return expression;
                    }

                case ExpressionType.Power:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Power(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.Quote:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.Quote(Replace(exp.Operand, @params));
                    }

                case ExpressionType.RightShift:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.RightShift(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.Subtract:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.Subtract(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.SubtractChecked:
                    {
                        var exp = (BinaryExpression)expression;
                        return Expression.SubtractChecked(Replace(exp.Left, @params), Replace(exp.Right, @params), exp.Method);
                    }

                case ExpressionType.TypeAs:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.TypeAs(Replace(exp.Operand, @params), exp.Type);
                    }

                case ExpressionType.TypeIs:
                    {
                        var exp = (TypeBinaryExpression)expression;
                        return Expression.TypeIs(Replace(exp.Expression, @params), exp.Type);
                    }

                case ExpressionType.UnaryPlus:
                    {
                        var exp = (UnaryExpression)expression;
                        return Expression.UnaryPlus(Replace(exp.Operand, @params), exp.Method);
                    }

                case ExpressionType.Default:
                case ExpressionType.Extension:
                case ExpressionType.Goto:
                case ExpressionType.Increment:
                case ExpressionType.Index:
                case ExpressionType.Label:
                case ExpressionType.RuntimeVariables:
                case ExpressionType.Loop:
                case ExpressionType.Switch:
                case ExpressionType.Throw:
                case ExpressionType.Try:
                case ExpressionType.Unbox:
                case ExpressionType.AddAssign:
                case ExpressionType.AndAssign:
                case ExpressionType.DivideAssign:
                case ExpressionType.ExclusiveOrAssign:
                case ExpressionType.LeftShiftAssign:
                case ExpressionType.ModuloAssign:
                case ExpressionType.MultiplyAssign:
                case ExpressionType.OrAssign:
                case ExpressionType.PowerAssign:
                case ExpressionType.RightShiftAssign:
                case ExpressionType.SubtractAssign:
                case ExpressionType.AddAssignChecked:
                case ExpressionType.MultiplyAssignChecked:
                case ExpressionType.SubtractAssignChecked:
                case ExpressionType.PreIncrementAssign:
                case ExpressionType.PreDecrementAssign:
                case ExpressionType.PostIncrementAssign:
                case ExpressionType.PostDecrementAssign:
                case ExpressionType.TypeEqual:
                case ExpressionType.OnesComplement:
                case ExpressionType.IsTrue:
                case ExpressionType.IsFalse:
                default:
                    throw new Exception($"The expression type \"{expression.NodeType}\" is not currently support.");
            }
        }

        private static ElementInit Replace(ElementInit init, (Expression ParamA, Expression ParamB)[] @params)
        {
            return init.Update(init.Arguments.Select(a => Replace(a, @params)));
        }

        private static MemberBinding Replace(MemberBinding binding, (Expression ParamA, Expression ParamB)[] @params)
        {
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    {
                        var assignment = (MemberAssignment)binding;
                        return Expression.Bind(assignment.Member, Replace(assignment.Expression, @params));
                    }

                case MemberBindingType.ListBinding:
                    {
                        var listBinding = (MemberListBinding)binding;
                        return Expression.ListBind(listBinding.Member, listBinding.Initializers.Select(i => Replace(i, @params)));
                    }

                case MemberBindingType.MemberBinding:
                    {
                        var memberBinding = (MemberMemberBinding)binding;
                        return Expression.MemberBind(memberBinding.Member, memberBinding.Bindings.Select(b => Replace(b, @params)));
                    }

                default:
                    throw new Exception($"The member binding type \"{binding.BindingType}\" is not currently support.");
            }
        }

        public static Expression Unwrap<TParam>(this Expression<Func<TParam, bool>> func, Expression param)
        {
            return Replace(func.Body, new (Expression, Expression)[] { (param, func.Parameters[0]) });
        }
    }
}
using System;

namespace Enriched.ActionExtended
{
    public static class ActionExtensions
    {
        public static Action NeutralizeException(this Action action)
        {
            return () =>
            {
                try
                {
                    action();
                }
                catch { }
            };
        }

        public static Action<object> ToActionObject<T>(this Action<T> actionT)
        {
            return actionT == null ? null : new Action<object>(o => actionT((T)o));
        }
    }
}
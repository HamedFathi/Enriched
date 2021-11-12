using System;
using System.Collections.Generic;
using System.Linq;

namespace Enriched.ExceptionExtended
{
    public static class ExceptionExtensions
    {
        private static IEnumerable<Exception> GetAllExceptions(this Exception exception)
        {
            yield return exception;

            if (exception is AggregateException aggrEx)
            {
                foreach (Exception innerEx in aggrEx.InnerExceptions.SelectMany(e => e.GetAllExceptions()))
                {
                    yield return innerEx;
                }
            }
            else if (exception.InnerException != null)
            {
                foreach (Exception innerEx in exception.InnerException.GetAllExceptions())
                {
                    yield return innerEx;
                }
            }
        }

        public static string ToFormattedString(this Exception exception)
        {
            IEnumerable<string> messages = exception
                .GetAllExceptions()
                .Where(e => !string.IsNullOrWhiteSpace(e.Message))
                .Select(e => e.Message.Trim());
            return string.Join(Environment.NewLine, messages);
        }
    }
}
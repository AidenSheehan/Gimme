using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace GimmeDigital.GoalCards
{
    public class CriteriaFactory
    {
        public record GoalStrings(string Desc, string Criteria, string Points);

        public static IEnumerable<Criterion> CreateCriteriaFromStrings(
            IEnumerable<string> strings
        ) => strings.Select(CreateCriterionFromString);

        private static Criterion CreateCriterionFromString(string criterion)
        {
            // Split string into method name and parameters
            criterion = criterion.Trim();
            int paramStartIndex = criterion.IndexOf('(');
            string methodName = criterion[0..paramStartIndex];
            string[] parameterStrings = criterion[(paramStartIndex + 1)..(criterion.Length - 1)]
                .Split(",");

            // Find Criterion method
            MethodInfo? method =
                GetCriterionByName(methodName)
                ?? throw new ArgumentException("Can't find criterion method");

            // Convert parameters string array to object array of correct types
            // TODO: Clean up, what if number of string parameters doesn't match method parameters?
            var paramInfo = method.GetParameters();
            object?[] parameters = new object?[parameterStrings.Length];
            for (int i = 0; i < parameterStrings.Length; i++)
            {
                ParameterInfo info = paramInfo[i];
                Type type = info.ParameterType;
                parameters[i] = ConvertParameter(parameterStrings[i], type);
            }

            Criterion? c = method.Invoke(null, parameters) as Criterion;
            return c ?? Criteria.Blank();
        }

        private static object ConvertParameter(string parameter, Type paramType)
        {
            if (paramType == typeof(int))
            {
                return int.Parse(parameter);
            }
            else if (paramType == typeof(Symbol))
            {
                return parameter.AsSymbol();
            }
            else if (paramType == typeof(Colour))
            {
                
                return parameter.AsColour();
            }
            else if (paramType == typeof(IEnumerable<int>))
            {
                var sec = parameter.Split("-");
                int start = int.Parse(sec[0]);
                int end = int.Parse(sec[1]);
                return Enumerable.Range(start, (end-start)+1);
            }
            else
            {
                return parameter;
            }
        }

        private static MethodInfo? GetCriterionByName(string name)
        {
            return typeof(Criteria).GetMethod(
                name,
                BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public
            );
        }
    }
}

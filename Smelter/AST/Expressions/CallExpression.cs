using Smelter.Interfaces;
using Smelter.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Smelter.AST.Expressions
{
    public class CallExpression : IExpression
    {
        public Token Token { get; set; }
        public IExpression Method { get; set; }
        public List<IExpression> Arguments { get; set; }

        public CallExpression(Token token, IExpression expression, List<IExpression> arguments)
        {
            Token = token;
            Method = expression;
            Arguments = arguments;
        }

        public IObj Evaluate(Environment environment)
        {
            var methodObj = Method.Evaluate(environment);
            if (methodObj is Err)
                return methodObj;

            var arguments = EvaluateArguments(environment);
            if (arguments.Count == 1 && arguments[0] is Err)
                return arguments[0];

            if (!(methodObj is Met))
                return new Err($"{methodObj.Type} не является методом!");

            var method = methodObj as Met;
            var innerEnvironment = new Environment(method, arguments);
            var evaluated = method.Body.Evaluate(innerEnvironment);

            if (evaluated is Ret)
                return (evaluated as Ret).Value;
            return evaluated;
        }

        private List<IObj> EvaluateArguments(Environment environment)
        {
            var result = new List<IObj>();

            foreach (var argument in Arguments)
            {
                var evaluated = argument.Evaluate(environment);
                if (evaluated is Err)
                    return new List<IObj>() { evaluated };

                result.Add(evaluated);
            }

            return result;
        }

        public override string ToString()
        {
            var arguments = string.Join(", ", Arguments.Select(x => x.ToString()));
            return $"{Method}({arguments})";
        }
    }
}

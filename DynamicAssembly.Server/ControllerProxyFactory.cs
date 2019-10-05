using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DynamicAssembly.Server
{
    public class ControllerProxyFactory
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ControllerProxyFactory(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        public T Create<T>() where T : class
        {
            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithoutTarget<T>(new CallInterceptor(CallService));
            return proxy;
        }

        private void CallService(IInvocation invocation)
        {
            var actionAttributes = invocation.Method.CustomAttributes.Select(x => x.AttributeType).ToList();
            string action = GetAction(invocation.Method);
            string controllerInterfaceName = invocation.Method.DeclaringType.Name;
            var controller = controllerInterfaceName.TrimStart('I').Replace("Controller", "");
            var argumentNames = invocation.Method.GetParameters().Select(p => p.Name).ToArray();
            var arguments = invocation.Arguments;
            var returnType = invocation.Method.ReturnType;

            Task.Run(async () => await Get(invocation, action, controller, argumentNames, arguments, returnType))
                .GetAwaiter().GetResult();
        }

        private string GetAction(MethodInfo method)
        {
            string action = method.Name;

            var routeAttribute = method.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(RouteAttribute));
            if (routeAttribute == null)
                return action;

            if (routeAttribute.ConstructorArguments.Count > 0)
            {
                return routeAttribute.ConstructorArguments.First().Value.ToString();
            }

            var nameArgument = routeAttribute.NamedArguments?.FirstOrDefault(x => x.MemberName == "Name");
            if (nameArgument == null)
                return action;

            return nameArgument.Value.TypedValue.Value.ToString();
        }

        private async Task Get(IInvocation invocation, string action, string controller, string[] argumentNames, object[] arguments, Type returnType)
        {
            /*string queryString = string.Join("&", Enumerable.Range(0, argumentNames.Length)
                .Select(i => new Tuple<string, string>(argumentNames[i], ParseArgument(arguments[i])))
                .Select(t => $"{t.Item1}={t.Item2.ToString()}"));*/

            string url = $"{_baseUrl}/api/{controller}";
            /*if (!string.IsNullOrWhiteSpace(queryString))
                url += "?" + queryString;*/

            var result = await Get(url);
            /*var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string resultAsString = await response.Content.ReadAsStringAsync();
            
            var resultInstance = JsonConvert.DeserializeObject(resultAsString, returnType);*/
            invocation.ReturnValue = result;
        }

        private async Task<string[]> Get(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string resultAsString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string[]>(resultAsString);
        }

        private class CallInterceptor : IInterceptor
        {
            private readonly Action<IInvocation> _intercept;

            public CallInterceptor(Action<IInvocation> intercept)
            {
                _intercept = intercept;
            }

            public void Intercept(IInvocation invocation)
            {
                _intercept(invocation);
            }
        }
    }
}

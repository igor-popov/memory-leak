using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Events.Server.Middlewares
{
    public class MagicMiddleware
    {
        private Func<IDictionary<string, object>, Task> _next;
        public void Initialize(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
            RegisterMe();
        }
               

        public async Task Invoke(IDictionary<string, object> environments)
        {
            var token = (CancellationToken)environments["host.OnAppDisposing"];

            if (token != CancellationToken.None)
            {
                token.Register(() =>
                {
                    UnregisterMe();
                });
            }

            await _next(environments);
        }

        private void RegisterMe()
        {
            System.Diagnostics.Trace.WriteLine("I am registered!");
        }

        private void UnregisterMe()
        {
            System.Diagnostics.Trace.WriteLine("I am unregistered!");
        }
    }
}
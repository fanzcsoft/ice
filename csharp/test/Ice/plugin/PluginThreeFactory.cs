// Copyright (c) ZeroC, Inc. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

namespace ZeroC.Ice.Test.Plugin
{
    public class PluginThreeFactory : IPluginFactory
    {
        public IPlugin Create(Communicator communicator, string name, string[] args) => new PluginThree(communicator);

        internal class PluginThree : BasePlugin
        {
            public PluginThree(Communicator communicator)
                : base(communicator)
            {
            }

            public override Task ActivateAsync(PluginActivationContext context, CancellationToken cancel)
            {
                var other = (BasePlugin?)Communicator.GetPlugin("PluginTwo");
                TestHelper.Assert(other != null);
                Other = other;
                TestHelper.Assert(Other.IsInitialized());
                Initialized = true;
                return Task.CompletedTask;
            }

            public override async ValueTask DisposeAsync()
            {
                if (!Destroyed)
                {
                    Destroyed = true;
                    await base.DisposeAsync();
                }
            }
        }
    }
}

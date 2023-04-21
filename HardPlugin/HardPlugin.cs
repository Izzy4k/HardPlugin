using Exiled.API.Features;
using HardPlugin.Events.Internal;
using HardPlugin.Features;
using System;
using ServerAPI = Exiled.Events.Handlers.Server;

namespace HardPlugin
{
    public class HardPlugin : Plugin<Config>
    {
        private static readonly Lazy<HardPlugin> LazyInstance = new Lazy<HardPlugin>(() => new HardPlugin());

        public static HardPlugin Instance => LazyInstance.Value;

        private static readonly Lazy<MineController> LazyMineController = new Lazy<MineController>(() => new MineController());

        public static MineController MineController => LazyMineController.Value;

        private HardServer server;

        public override void OnEnabled()
        {
            InitData();
            RegisterEvents();
            base.OnEnabled();
        }

        private void RegisterEvents()
        {
            ServerAPI.WaitingForPlayers += server.OnWaitingForPlayers;
            ServerAPI.RoundStarted += server.OnRoundStart;
        }

        private void UnInitData()
        {
            server = null;
        }

        private void UnRegisterEvents()
        {
            ServerAPI.WaitingForPlayers -= server.OnWaitingForPlayers;
            ServerAPI.RoundStarted -= server.OnRoundStart;
        }

        private void InitData()
        {
            server = new HardServer();
        }

        public override void OnDisabled()
        {
            UnInitData();
            UnRegisterEvents();
            base.OnDisabled();
        }
    }
}
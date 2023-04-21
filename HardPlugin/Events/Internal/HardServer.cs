using Exiled.API.Features;

namespace HardPlugin.Events.Internal
{
    public class HardServer
    {
        public void OnWaitingForPlayers()
        {
            Log.Info($"Waiting for player");
        }

        public void OnRoundStart()
        {
            HardPlugin.MineController.CreateMines(HardPlugin.Instance.Config.CountMines);
        }
    }
}

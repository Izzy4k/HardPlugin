using Exiled.API.Features;
using Exiled.CustomItems.API;
using HardPlugin.Api;
using System;

namespace HardPlugin.Features
{
    public class ItemController
    {
        public void OnSpawnCoin(Player player)
        {
            uint id = Convert.ToUInt32(player.Id);
            var coin = new MyCoin(id);
            coin.Register();
            coin.Give(player);
        }
    }
}

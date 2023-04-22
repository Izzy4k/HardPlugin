using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Items;
using Mirror;
using Exiled.API.Features;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System;
using InventorySystem.Items;

namespace HardPlugin.Api
{
    internal class MyCoin : CustomItem
    {
        public override uint Id { get; set; } = 0;

        public override string Name { get; set; } = "MyTopCoin";

        public override string Description { get; set; } = "Волшебная монетка. Кинь её и все узнаешь";

        public override float Weight { get; set; } = 1f;

        public override ItemType Type { get; set; } = ItemType.Coin;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();

        public MyCoin(uint id)
        {
            Id = id;
        }
        protected override void OnDropping(DroppingItemEventArgs ev)
        {
            UseAbility(ev.Player.Position, ev.Item);

            base.OnDropping(ev);
        }

        private void UseAbility(Vector3 pos, Item item)
        {
            List<Pickup> pickupsDelete = new List<Pickup>();

            foreach (var pickup in Pickup.List)
            {
                float distance = Vector3.Distance(pos, pickup.Position);

                if (distance <= HardPlugin.Instance.Config.RadiusCoin)
                {
                    Timing.RunCoroutine(TeletorpPickup(pickup, pos));

                    pickupsDelete.Add(pickup);
                }
            }

            Timing.CallDelayed(2f, () =>
                            DeleteItems(pickupsDelete, pos)
            );
        }

        private IEnumerator<float> TeletorpPickup(Pickup pickup, Vector3 pos)
        {
            yield return Timing.WaitForSeconds(0.2f);
            pickup.Position = pos;
        }

        private void DeleteItems(List<Pickup> items, Vector3 pos)
        {
            foreach (var pickup in items)
            {
                pickup.Destroy();
            }

            DeleteCoin(pos);
        }

        private void DeleteCoin(Vector3 pos)
        {
            foreach (var pickup in Pickup.List)
            {
                float distance = Vector3.Distance(pos, pickup.Position);

                if (distance <= HardPlugin.Instance.Config.RadiusCoin)
                {
                    pickup.Destroy();
                }
            }
        }
    }
}

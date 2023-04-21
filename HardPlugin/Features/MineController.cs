using Exiled.API.Features;
using System.Linq;
using UnityEngine;
using Exiled.API.Features.Toys;
using System.Collections.Generic;
using Exiled.API.Features.Items;
using HardPlugin.Api;

namespace HardPlugin.Features
{
    public class MineController
    {
        private Dictionary<GameObject, Primitive> primitives = new Dictionary<GameObject, Primitive>();

        public void CreateMines(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnMine();
            }
        }

        private void SpawnMine()
        {
            var randomRoom = GetRandomRoom();
            var position = randomRoom.Position;

            var primitive = createPrimitive(position);

            primitives.Add(primitive.Base.gameObject, primitive);

            primitive.Spawn();
        }

        private Room GetRandomRoom()
        {
            var allRooms = Room.List.ToList();

            return allRooms[Random.Range(0, allRooms.Count)];
        }

        private Primitive createPrimitive(Vector3 pos)
        {
            var primitive = Primitive.Create(PrimitiveType.Cube, new Vector3(pos.x, pos.y + 0.2f, pos.z), scale: new Vector3(0.5f, 0.5f, 0.5f),spawn: false);

            primitive.Base.gameObject.AddComponent<BoxCollider>();
            var collider = primitive.Base.gameObject.GetComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = new Vector3(2f, 2f, 2f);

            primitive.Base.gameObject.AddComponent<Mine>();

            return primitive;
        }

        private void SpawnGrenade(Vector3 position)
        {
            ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
            grenade.FuseTime = 0;
            grenade.SpawnActive(position);
        }

        public void DeletePrimitive(GameObject gameObject)
        {
            if (!primitives.ContainsKey(gameObject)) return;

            var primitive = primitives[gameObject];

            primitive.Destroy();

            primitives.Remove(gameObject);

            SpawnGrenade(gameObject.transform.position);
        }
    }
}

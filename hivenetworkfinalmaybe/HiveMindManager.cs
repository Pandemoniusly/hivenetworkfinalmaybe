using Unity.Netcode;
using UnityEngine;

namespace hivebombnetcode
{
    public class HiveMindManager : NetworkBehaviour
    {
        public static HiveMindManager instance;

        void Awake()
        {
            instance = this;
        }

        [ServerRpc]
        public static void ExplodeAtServerRpc(Transform transform, float rand, bool knockback, bool visible, int dmg, float rad)
        {
            ExplodeAtClientRpc(transform, rand, knockback, visible, dmg, rad);
        }

        [ClientRpc]
        public static void ExplodeAtClientRpc(Transform transform, float rand, bool knockback, bool visible, int dmg, float rad)
        {
            Landmine.SpawnExplosion(transform.position, visible, 0, rad, dmg, knockback ? rand : 0);
        }
    }
}


using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Spawn_rate_scaling
{
    public class SpawnConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // Removing [Range] and [Slider] defaults these to a simple text input box
        
        [DefaultValue(3)]
        public int BaseMultiplier;

        [DefaultValue(1)]
        public int IncreasePerBoss;

        [DefaultValue(50)]
        public int MaxMultiplierCap;
    }
}
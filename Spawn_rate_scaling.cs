using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace Spawn_rate_scaling
{
    public class SpawnRateSystem : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            var config = ModContent.GetInstance<SpawnConfig>();
            int bossesDowned = GetBossCount();

            // We cast to float here so the math remains precise
            float multiplier = (float)config.BaseMultiplier + (bossesDowned * (float)config.IncreasePerBoss);

            if (multiplier > config.MaxMultiplierCap)
                multiplier = config.MaxMultiplierCap;

            spawnRate = (int)(spawnRate / multiplier);
            maxSpawns = (int)(maxSpawns * multiplier);

            if (spawnRate < 1) spawnRate = 1;
        }

        public static int GetBossCount()
        {
            int count = 0;
            if (NPC.downedSlimeKing) count++;
            if (NPC.downedBoss1) count++; 
            if (NPC.downedBoss2) count++; 
            if (NPC.downedQueenBee) count++;
            if (NPC.downedBoss3) count++; 
            if (NPC.downedDeerclops) count++;
            if (Main.hardMode) count++;    
            if (NPC.downedQueenSlime) count++;
            if (NPC.downedMechBoss1) count++; 
            if (NPC.downedMechBoss2) count++; 
            if (NPC.downedMechBoss3) count++; 
            if (NPC.downedPlantBoss) count++; 
            if (NPC.downedGolemBoss) count++; 
            if (NPC.downedEmpressOfLight) count++;
            if (NPC.downedFishron) count++;   
            if (NPC.downedAncientCultist) count++; 
            if (NPC.downedMoonlord) count++;
            return count;
        }
    }

    public class BossDeathMessageSystem : ModSystem
    {
        private int lastBossCount = -1;

        public override void PostUpdateWorld()
        {
            if (Main.netMode == NetmodeID.Server || Main.netMode == NetmodeID.SinglePlayer)
            {
                int currentBossCount = SpawnRateSystem.GetBossCount();

                if (lastBossCount != -1 && currentBossCount > lastBossCount)
                {
                    var config = ModContent.GetInstance<SpawnConfig>();
                    float currentMult = (float)config.BaseMultiplier + (currentBossCount * (float)config.IncreasePerBoss);
                    
                    Main.NewText($"Difficulty Increased! Current Spawn Multiplier: {currentMult}x", 255, 50, 50);
                }

                lastBossCount = currentBossCount;
            }
        }

        public override void OnWorldLoad()
        {
            lastBossCount = SpawnRateSystem.GetBossCount();
        }
    }
}
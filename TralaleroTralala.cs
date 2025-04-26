using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TralaleroTralala
{
    public class TralaleroTralala : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Primary;

        public override string BaseTower => TowerType.DartMonkey;

        public override string Icon => Portrait;

        public override int Cost => 750;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.range = 20;
            towerModel.GetAttackModel().range = 20;

            var wpn = towerModel.GetWeapon();
            wpn.rate = 1.2f;
            wpn.projectile.display = new("");

            wpn.projectile.pierce = 99;

            towerModel.AddBehavior(new LinkProjectileRadiusToTowerRangeModel("LinkProjectileRadiusToTowerRangeModel_", wpn.projectile, 20, 0, 0));
        }
    }

    #region Upgrades

    public abstract class TralaleroUpgrade : ModUpgrade<TralaleroTralala>
    {
        public override int Path => Top;
    }

    public class RealNIKEKicks : TralaleroUpgrade
    {
        public override int Tier => 1;

        public override int Cost => 520;

        public override string DisplayName => "Real NIKE™ Kicks";

        public override string DisplayNamePlural => DisplayName;

        public override string Description => "Real NIKE™ Kicks makes Tralalero Tralala better at attacking! However they are heavier and make Tralalero Tralala attack a little slower.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.IncreaseRange(5);

            var wpn = towerModel.GetWeapon();
            wpn.projectile.GetDamageModel().damage += 1;
            wpn.rate += 0.1f;
        }
    }
    public class AwesomeAirforces : TralaleroUpgrade
    {
        public override int Tier => 2;

        public override int Cost => 1150;

        public override string DisplayNamePlural => DisplayName;

        public override string Description => "Light shoes for swifty attacks! Tralalero Tralala can now hit camos.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.IncreaseRange(5);

            var wpn = towerModel.GetWeapon();
            wpn.rate /= 2;
            towerModel.GetDescendants<FilterInvisibleModel>().ForEach(mod => mod.isActive = false);
        }
    }
    public class FlappyShark : TralaleroUpgrade
    {
        public override int Tier => 3;

        public override int Cost => 2300;

        public override string Icon => Portrait;

        public override string Description => "Tralalero Tralala now jumps high into the air to attack! Does extra damage to bombo- I mean MOABS. Also crushes Lead and Ceramic bloons.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.IncreaseRange(10);

            var wpn = towerModel.GetWeapon();
            wpn.projectile.GetDamageModel().damage += 1;

            wpn.projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;
            wpn.projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_MOABS", "Moabs", 1.25f, 1, false, true));
            wpn.projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Ceramic", "Ceramic", 1, 2, false, true));
            wpn.projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_MOABS", "Lead", 1, 2, false, true));
        }
    }
    public class BallNChain : TralaleroUpgrade
    {
        public override int Tier => 4;

        public override int Cost => 6750;
        public override string DisplayName => "Ball n' Chain";

        public override string Description => "Ball n' Chain makes Tralalero Tralala even stronger! Making their... shockwaves from their jumps even more powerful! They also fall faster I guess.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.IncreaseRange(10);

            var wpn = towerModel.GetWeapon();
            wpn.projectile.GetDamageModel().damage += 4;

            wpn.rate *= 0.8f;

            wpn.projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_MOABS", "Moabs", 1.25f, 0, false, true));
        }
    }
    public class MikeSharkson : TralaleroUpgrade
    {
        public override int Tier => 5;

        public override int Cost => 32560;
        public override string DisplayName => "Mike Sharkson";

        public override string Description => "Tralalero Tralala decided to become a Mike Tyson wannabe. He's on his (her?) boxing arc!! (imagine going by seasons and episodes instead of arcs)";

        public override string Icon => Portrait;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.IncreaseRange(-10);

            var wpn = towerModel.GetWeapon();
            wpn.projectile.GetDamageModel().damage += 10;

            wpn.rate *= 0.6f;

            wpn.projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_MOABS", "Moabs", 1.6f, 0, false, true));
        }
    }

    #endregion

    #region Display
    public class Shark000 : SharkDisplay
    {
        public override bool UseForTower(params int[] tiers) => tiers[0] < 3;
    }
    public class Shark300 : SharkDisplay
    {
        public override bool UseForTower(params int[] tiers) => tiers[0] >= 3 && tiers[0] < 5;
    }
    public class Shark500 : SharkDisplay
    {
        public override bool UseForTower(params int[] tiers) => tiers[0] == 5;
    }

    public abstract class SharkDisplay : ModTowerCustomDisplay<TralaleroTralala>
    {
        public override string AssetBundleName => "tralalero";

        public override string PrefabName => Name;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (var renderer in node.GetMeshRenderers())
            {
                renderer.ApplyOutlineShader();
                renderer.SetOutlineColor(new Color32(38, 38, 38, 255));
            }
        }
    }
    #endregion
}

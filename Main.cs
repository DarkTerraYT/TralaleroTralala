using MelonLoader;
using BTD_Mod_Helper;
using TralaleroTralala;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation.Towers;
using BTD_Mod_Helper.Extensions;
using UnityEngine.Playables;
using BTD_Mod_Helper.Api;

[assembly: MelonInfo(typeof(TralaleroTralala.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TralaleroTralala;

public class Main : BloonsTD6Mod
{
}

[HarmonyPatch(typeof(Weapon), nameof(Weapon.Emit))]
static class Weapon_SpawnDart
{
    public static void Postfix(Weapon __instance, Tower owner)
    {
        if (owner.towerModel.baseId == ModContent.TowerID<TralaleroTralala>())
        {
            owner.GetUnityDisplayNode().animationComponent.SetTriggerString("Attack");
        }
    }
}
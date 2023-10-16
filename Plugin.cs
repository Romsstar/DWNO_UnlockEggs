using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace UnlockEggs;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "Romsstar.DWNO.UnlockEggs";
    internal const string PluginName = "UnlockEggs";
    internal const string PluginVersion = "1.0.0";

    public override void Load()
    {
        Awake();
    }

    public void Awake()
    {
        Harmony harmony = new Harmony("Patches");
        harmony.PatchAll();
    }

    [HarmonyPatch(typeof(AppMainScript), "_FinishedParameterLoad")]
    public static class UnlockEvo
    {
        [HarmonyPrefix]
        public static void Prefix(AppMainScript __instance)
        {
            foreach (ParameterDigimonData param in __instance.m_parameters.digimonData.m_params)
            {
                param.m_dlc_flagset = 4011245244;
                param.m_evo1_flagset = 4011245244;
                param.m_dlc_no = 0;
            }
        }
    }

    [HarmonyPatch(typeof(uRebirthPanel), "enablePanel")]
    public static class UnlockEgg
    {
        [HarmonyPostfix]
        public static void Postfix(uRebirthPanel __instance, bool enable)
        {
            __instance.eggMax = 13;
            __instance.m_eggTbl = new int[__instance.eggMax];

            int i;
            for (i = 0; i < 8; i++)
            {
                __instance.m_eggTbl[i] = i + 1;
            }

            __instance.m_eggTbl[i++] = 10;
            __instance.m_eggTbl[i++] = 12;
            __instance.m_eggTbl[i++] = 9;
            __instance.m_eggTbl[i++] = 13;
            __instance.m_eggTbl[i++] = 14;
            __instance.InitializeDiagram(0, true);
        }
    }
}
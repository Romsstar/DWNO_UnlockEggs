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

    [HarmonyPatch(typeof(uRebirthPanel), "enablePanel")]
    [HarmonyPostfix]
    public static void Postfix(uRebirthPanel __instance, bool enable)
    {
        uint flagSetId = Language.makeHash("flagset_166");
        StorageData.m_ScenarioProgressData.SetScenarioFlagByFlagSet(flagSetId, true);

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
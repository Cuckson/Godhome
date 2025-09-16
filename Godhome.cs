using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using GlobalSettings;
using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace silksong_godhome_mod;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public sealed class Godhome : BaseUnityPlugin
{
    public static Godhome _instance = null!;
    private string sceneInput = "";
    private string gateInput  = "";
    
    public static ConfigEntry<KeyboardShortcut> ToggleMenuKey;
    
    private void Awake()
    {
        _instance = this;
        var harmony = new Harmony("cuckson.silksong-godhome");
        harmony.PatchAll();
        

        ToggleMenuKey = Config.Bind(
            "Controls",
            "Toggle Menu",
            new KeyboardShortcut(KeyCode.B),
            "Key used to open/close the Godhome menu"
        );
        
        var menuGO = new GameObject("GodhomeMenu");
        
        DontDestroyOnLoad(menuGO);
        
        menuGO.AddComponent<GodhomeMenuGUI>();
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    public static void DisableAllBoolsExcept(PlayerData data, params string[] exclude)
    {
        if (data == null) return;

        var type = typeof(PlayerData);
        var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        foreach (var field in type.GetFields(flags))
        {
            if (field.FieldType == typeof(bool) &&
                !exclude.Contains(field.Name, StringComparer.OrdinalIgnoreCase))
            {
                try
                {
                    field.SetValue(data, false);
                }
                catch (Exception e)
                {
                }
            }
        }
    }
    
    private void Update()
    {
        if (GodhomeMenuGUI.menuVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    IEnumerator FixBossScenes()
    {
        yield return new WaitForSeconds(1f);
        
        // tormented trobbio
        GameObject trobbiostage = GameObject.Find("Grand Stage Scene");
        if (trobbiostage != null)
        {
            var go = trobbiostage.transform.Find("Boss Scene TormentedTrobbio").gameObject;
            go.SetActive(true);
        }
        
        // moss mother
        GameObject target = GameObject.Find("Black Thread States");
        if (target != null)
        {
            Transform? t = target.transform
                .Find("Normal World")
                ?.Find("Battle Scene")
                ?.Find("Wave 1");
            if (t != null)
                t.gameObject.SetActive(true);
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GodhomeMenuGUI.menuVisible = false;
        
        if (GodhomeMenuGUI.panelGO != null)
            GodhomeMenuGUI.panelGO.SetActive(false);
        
        StartCoroutine(FixBossScenes());
    }
    
    [HarmonyPatch(typeof(InputHandler), "SetCursorEnabled")]
    internal static class Patch_SetCursorEnabled
    {
        private static bool Prefix(ref bool isEnabled)
        {
            if (GodhomeMenuGUI.menuVisible) 
            {
                isEnabled = true; 
            }
            return true; 
        }
    }
    
    [HarmonyPatch(typeof(InputHandler), "Update")]
    class Patch_InputHandler_Update
    {
        private static void Postfix()
        {
            if (GodhomeMenuGUI.menuVisible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    
    
    [HarmonyPatch(
        typeof(GameManager),
        nameof(GameManager.SaveGame),
        new Type[] {
            typeof(int),
            typeof(Action<bool>),
            typeof(bool),
            typeof(AutoSaveName)
        }
    )]
    internal static class Patch_SaveGame_Nullify
    {
        private static bool Prefix(int saveSlot,
            Action<bool> ogCallback,
            bool withAutoSave,
            AutoSaveName autoSaveName)
        {
            ogCallback?.Invoke(true);
            return false;
        }
    }
}

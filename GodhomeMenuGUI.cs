using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx.Configuration;
using GlobalEnums;
using UnityEngine;
using UnityEngine.UI;

namespace silksong_godhome_mod
{
    public class GodhomeMenuGUI : MonoBehaviour
    {
        private GameObject canvasGO;
        public static GameObject panelGO;
        public static bool menuVisible = false;

        private struct SceneOption
        {
            public string displayName;
            public string sceneName;
            public string gateName;
            public Action onSelect;
        }

        private List<SceneOption> options = new List<SceneOption>()
        {
            new SceneOption { displayName = "Safe Zone", sceneName = "Bone_East_Umbrella", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Moss Mother", sceneName = "Tut_03", gateName = "right1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.encounteredMossMother = false;
                    GameManager.instance.playerData.defeatedMossMother = false;
                }
            },
            new SceneOption { displayName = "Bell Beast", sceneName = "Bone_05", gateName = "left1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.encounteredBellBeast = true;
                    GameManager.instance.playerData.defeatedBellBeast = false;
                    GameManager.instance.playerData.blackThreadWorld = true;
                }
            },
            new SceneOption { displayName = "Lace", sceneName = "Bone_East_12", gateName = "bot1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.encounteredLace1 = true;
                }
            },
            new SceneOption { displayName = "Fourth Chorus", sceneName = "Bone_East_08", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Savage Beastfly", sceneName = "Ant_19", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Sister Splinter", sceneName = "Shellwood_18", gateName = "right1", onSelect = () => {}
            },
            new SceneOption { displayName = "Skull Tyrant", sceneName = "Bone_15", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Widow", sceneName = "Belltown_Shrine", gateName = "top1", onSelect = () => {}
            },
            new SceneOption { displayName = "Moorwing", sceneName = "Greymoor_08", gateName = "top1", onSelect = () => {}
            },
            new SceneOption { displayName = "Last Judge", sceneName = "Coral_Judge_Arena", gateName = "door2", onSelect =
                () =>
                {
                    GameManager.instance.playerData.bellShrineBellhart = true;
                    GameManager.instance.playerData.bellShrineEnclave = true;
                    GameManager.instance.playerData.bellShrineGreymoor = true;
                    GameManager.instance.playerData.bellShrineShellwood = true;
                    GameManager.instance.playerData.bellShrineWilds = true;
                    GameManager.instance.playerData.bellShrineBoneForest = true;
                    GameManager.instance.playerData.encounteredLastJudge = true;
                }
            },
            new SceneOption { displayName = "Great Conchflies", sceneName = "Coral_11", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Phantom", sceneName = "Organ_01", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Trobbio", sceneName = "Library_13", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Tormented Trobbio", sceneName = "Library_13", gateName = "left1", onSelect = () =>
                {
                    GameManager.instance.playerData.blackThreadWorld = true;
                }
            },
            new SceneOption { displayName = "Cogwork Dancers", sceneName = "Cog_Dancers", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Second Sentinel", sceneName = "Hang_17b", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Crawfather", sceneName = "Room_CrowCourt_02", gateName = "top1", onSelect = () => {}
            },
            new SceneOption { displayName = "Raging Conchfly", sceneName = "Coral_27", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Seth", sceneName = "Shellwood_22", gateName = "right1", onSelect = () => {}
            },
            new SceneOption { displayName = "Grand Mother Silk", sceneName = "Cradle_03", gateName = "right2", onSelect = () => {}
            },
            new SceneOption { displayName = "Clover Dancers", sceneName = "Clover_10", gateName = "left1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.encounteredCloverDancers = true;
                }
            },
            new SceneOption { displayName = "Lace 2", sceneName = "Song_Tower_01", gateName = "door_cutsceneEndLaceTower", onSelect =
                () =>
                {
                    GameManager.instance.playerData.encounteredLaceTower = true;
                    GameManager.instance.playerData.defeatedLaceTower = false;
                }
            },
            new SceneOption { displayName = "Karmelita", sceneName = "Memory_Ant_Queen", gateName = "door_wakeInMemory", onSelect =
                () =>
                {
                    GameManager.instance.playerData.defeatedAntQueen = false;
                }
            },
            new SceneOption { displayName = "Crust King Khann", sceneName = "Memory_Coral_Tower", gateName = "door_wakeInMemory", onSelect =
                () =>
                {
                    GameManager.instance.playerData.defeatedCoralKing = false;
                }
            },
            new SceneOption { displayName = "Nyleth", sceneName = "Shellwood_11b_Memory", gateName = "door_wakeInMemory", onSelect =
                () =>
                {
                    GameManager.instance.playerData.defeatedFlowerQueen = false;
                }
            },
            new SceneOption { displayName = "Groal The Great", sceneName = "Shadow_18", gateName = "right1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.DefeatedSwampShaman = false;
                }
            },
            new SceneOption { displayName = "Chef Lugoli", sceneName = "Dust_Chef", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Broodmother", sceneName = "Slab_16b", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Giant Flea", sceneName = "Arborium_08", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Garmond and Zaza", sceneName = "Library_09", gateName = "left1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.garmondInLibrary = true;
                }
            },
            new SceneOption { displayName = "Watcher at the Edge", sceneName = "Coral_39", gateName = "right1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.wokeGreyWarrior = true;
                }
            },
            new SceneOption { displayName = "Palestag", sceneName = "Clover_19", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Forebrothers Signis and Gron", sceneName = "Dock_09", gateName = "right1", onSelect = () => {}
            },
            new SceneOption { displayName = "The Unraveled", sceneName = "Ward_02", gateName = "right1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.wardBossEncountered = false;
                    GameManager.instance.playerData.wardBossHatchOpened = false;
                    GameManager.instance.playerData.collectedWardBossKey = true;
                }
            },
            new SceneOption { displayName = "Gurr the Outcast", sceneName = "Bone_East_18b", gateName = "top1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.encounteredAntTrapper = true;
                }
            },
            new SceneOption { displayName = "Voltvyrm", sceneName = "Coral_29", gateName = "left1", onSelect =
                () =>
                {
                }
            },
            new SceneOption { displayName = "Shakra", sceneName = "Greymoor_08", gateName = "left2", onSelect =
                () =>
                {
                    GameManager.instance.playerData.mapperSparIntro = true;
                    GameManager.instance.playerData.mapperMentorConvo = true;
                }
            },
            new SceneOption { displayName = "Pinstress", sceneName = "Peak_07", gateName = "top2", onSelect =
                () =>
                {
                    var data = GameManager.instance.playerData.QuestCompletionData.RuntimeData;
                    GameManager.instance.playerData.QuestCompletionData.SetData("Pinstress Battle", QuestCompletionData.Accepted);
                    GameManager.instance.playerData.blackThreadWorld = true;
                }
            },
            new SceneOption { displayName = "Father of the Flame", sceneName = "Wisp_02", gateName = "left1", onSelect = () => {}
            },
            new SceneOption { displayName = "Bell Eater", sceneName = "Bellway_Centipede_Arena", gateName = "top1", onSelect = () => {}
            },
            new SceneOption { displayName = "Lost Lace", sceneName = "Abyss_Cocoon", gateName = "door_entry", onSelect = () => {}
            },
            new SceneOption { displayName = "Lost Garmond", sceneName = "Coral_33", gateName = "right1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.blackThreadWorld = true;
                    GameManager.instance.playerData.garmondEncounters_act3 = 99;
                }
            },
            new SceneOption { displayName = "First Sinner", sceneName = "Slab_10b", gateName = "left1", onSelect =
                () =>
                {
                    GameManager.instance.playerData.encounteredFirstWeaver = false;
                    GameManager.instance.playerData.defeatedFirstWeaver = false;
                    GameManager.instance.playerData.wokeLiftWeaver = false;
                } 
            },
        };

        void Awake()
        {
            CreateMenu();
            ToggleMenu(false);
        }

        void Update()
        {
            if (Godhome.ToggleMenuKey.Value.IsDown() && GameManager.instance.hero_ctrl != null)
            {
                menuVisible = !menuVisible;
                ToggleMenu(menuVisible);

                if (menuVisible)
                    UIManager.instance.inputModule.allowMouseInput = true;
            }

            if (menuVisible)
                RecalculateMenuLayout();
        }
        
        private void RecalculateMenuLayout()
        {
            if (panelGO == null) return;

            float screenHeight = Screen.height;
            float screenWidth = Screen.width;
            float padding = 10f;
            float buttonHeight = 40f;
            float buttonWidth = 200f;
            int maxButtonsPerColumn = Mathf.FloorToInt((screenHeight - padding) / (buttonHeight + padding));

            var panelRect = panelGO.GetComponent<RectTransform>();
            panelRect.sizeDelta = Vector2.zero;
            int currentColumn = 0;
            int currentRow = 0;

            for (int i = 0; i < panelGO.transform.childCount; i++)
            {
                var buttonGO = panelGO.transform.GetChild(i).gameObject;
                var rect = buttonGO.GetComponent<RectTransform>();
                
                float xPos = -padding - currentColumn * (buttonWidth + padding);
                float yPos = -padding - currentRow * (buttonHeight + padding);

                rect.pivot = new Vector2(1f, 1f);
                rect.anchorMin = new Vector2(1f, 1f);
                rect.anchorMax = new Vector2(1f, 1f);
                rect.sizeDelta = new Vector2(buttonWidth, buttonHeight);
                rect.anchoredPosition = new Vector2(xPos, yPos);

                currentRow++;
                if (currentRow >= maxButtonsPerColumn)
                {
                    currentRow = 0;
                    currentColumn++;
                }
            }
            
            float panelWidth = (currentColumn + 1) * (buttonWidth + padding);
            float panelHeight = Mathf.Min(maxButtonsPerColumn, panelGO.transform.childCount) * (buttonHeight + padding);
            panelRect.sizeDelta = new Vector2(panelWidth + padding, panelHeight + padding);
        }
        
        private void CreateMenu()
        {
            float buttonHeight = 50f;
            float padding = 10f;
            float currentY = 0f;
            
            float columnSpacing = 260f; 
            int buttonsPerColumn = Mathf.FloorToInt((Screen.height - 40) / (buttonHeight + padding));
            int column = 0;
            int row = 0;
            
            canvasGO = new GameObject("GodhomeMenuCanvas");
            DontDestroyOnLoad(canvasGO);
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            
            panelGO = new GameObject("Panel");
            panelGO.transform.SetParent(canvasGO.transform, false);
            var panelImage = panelGO.AddComponent<Image>();
            panelImage.color = new Color(0, 0, 0, 0f);

            var panelRect = panelGO.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(1f, 1f);
            panelRect.anchorMax = new Vector2(1f, 1f);
            panelRect.pivot = new Vector2(1f, 1f);
            panelRect.anchoredPosition = new Vector2(-20f, -20f);
            int totalColumns = Mathf.CeilToInt((float)options.Count / buttonsPerColumn);
            panelRect.sizeDelta = new Vector2(totalColumns * columnSpacing, Screen.height - 40f);

            foreach (var opt in options)
            {
                var buttonGO = new GameObject(opt.displayName + "_Button");
                buttonGO.transform.SetParent(panelGO.transform, false);

                var rect = buttonGO.AddComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(1, 1);
                rect.pivot = new Vector2(0.5f, 1f);
                rect.sizeDelta = new Vector2(0, buttonHeight);
                row = options.IndexOf(opt) % buttonsPerColumn;
                column = options.IndexOf(opt) / buttonsPerColumn;
                rect.anchoredPosition = new Vector2(-column * columnSpacing, -row * (buttonHeight + padding));

                currentY += buttonHeight + padding;

                var image = buttonGO.AddComponent<Image>();
                image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

                var button = buttonGO.AddComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    StartCoroutine(TransitionToScene(opt.sceneName, opt.gateName));
                    
                    var pd = PlayerData.instance;
                    
                    Godhome.DisableAllBoolsExcept(pd,
                        "hasDash",
                        "hasChargeSlash",
                        "HasBoundCrestUpgrader",
                        "UnlockedExtraBlueSlot",
                        "UnlockedExtraYellowSlot",
                        "HasWhiteFlower",
                        "hasWalljump",
                        "hasDoubleJump",
                        "hasBrolly",
                        "hasSuperJump",
                        "HasSeenEvaHeal",
                        "hasHarpoonDash",
                        "hasQuill",
                        "UnlockedFastTravel",
                        "UnlockedFastTravelTeleport",
                        "hasNeedolinMemoryPowerup",
                        "hasNeedolin",
                        "mapAllRooms",
                        "HasAllMaps");
                    
                    opt.onSelect?.Invoke();
                });

                var textGO = new GameObject("Text");
                textGO.transform.SetParent(buttonGO.transform, false);
                var text = textGO.AddComponent<Text>();
                text.text = opt.displayName;
                text.alignment = TextAnchor.MiddleCenter;
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                text.color = Color.white;

                var textRect = textGO.GetComponent<RectTransform>();
                textRect.anchorMin = Vector2.zero;
                textRect.anchorMax = Vector2.one;
                textRect.offsetMin = Vector2.zero;
                textRect.offsetMax = Vector2.zero;
                
                var colors = button.colors;
                colors.normalColor = image.color;
                colors.highlightedColor = new Color(0.4f, 0.4f, 0.4f, 0.9f);
                colors.pressedColor = new Color(0.1f, 0.1f, 0.1f, 1f);
                button.colors = colors;
            }
        }

        private void ToggleMenu(bool visible)
        {
            if (panelGO != null)
                panelGO.SetActive(visible);
        }

        IEnumerator TransitionToScene(string targetScene, string targetGate)
        {
            ScreenFaderUtils.Fade(ScreenFaderUtils.GetColour(), Color.black, 1f);
            yield return new WaitForSeconds(1f + 1f);
            
            foreach (var src in FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID))
            {
                src.Stop();
            }
            
            GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo {
                SceneName = targetScene,
                EntryGateName = targetGate,
                Visualization = GameManager.SceneLoadVisualizations.Default,
                PreventCameraFadeOut = true
            });
        }
    }
}
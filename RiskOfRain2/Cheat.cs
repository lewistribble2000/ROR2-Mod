using HG;
using RoR2;
using RoR2.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RiskOfRain2
{
    public class Cheat : MonoBehaviour
    {
        bool showConsole = false;
        bool applyGodmode = false;
        bool applySpeed = false;
        bool applyDamageBoost = false;
        bool scanForChests = false;
        bool applyFly = true;

        float playerBaseMoveSpeed;
        float playerBaseDamage;

        ChestBehavior[] chestBehaviors = FindObjectsOfType<ChestBehavior>();
        NetworkUser[] networkUsers = FindObjectsOfType<NetworkUser>();
        CharacterMaster[] master = FindObjectsOfType<CharacterMaster>();
        CharacterMaster localPlayer;
        CharacterBody localPlayerBody;
        CharacterMotor localPlayerMotor;
        List<Transform> chestTransforms = new List<Transform>();

        private void Start()
        {
            Debug.ClearDeveloperConsole();
            localPlayer = GetLocalPlayerMaster(master);
            localPlayerBody = localPlayer.GetBody();
            localPlayerMotor = localPlayerBody.characterMotor;
            playerBaseMoveSpeed = localPlayerBody.baseMoveSpeed;
            playerBaseDamage = localPlayerBody.baseDamage;
            InvokeRepeating(nameof(FetchAllObjects), 0f, 10f);
        }
        private void Update()
        {
            foreach (var chest in chestBehaviors)
            {
                chestTransforms.Add(chest.transform);
            }

            if(applyFly)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    localPlayerMotor.Jump(2f, 2f);
                }
            }
            CheckMenuInteraction();
        }

        private void OnGUI()
        {
            float y = 0f;
            if(showConsole)
            {
                GUI.Box(new Rect(0, y, Screen.width, 30), "");
                GUI.Box(new Rect(0, y + 20, Screen.width, 50), "Apply Godmode (F2): " + applyGodmode.ToString());
                GUI.Box(new Rect(0, y + 40, Screen.width, 50), "Apply Speed (F3): " + applySpeed.ToString());
                GUI.Box(new Rect(0, y + 60, Screen.width, 50), "Apply Damage (F4): " + applyDamageBoost.ToString());
                GUI.Box(new Rect(0, y + 80, Screen.width, 50), "Give Gold (F5):" + "Add 1000 gold");
                GUI.Box(new Rect(0, y + 90, Screen.width, 50), "Scan Chests (F6): " + scanForChests.ToString());
                GUI.Box(new Rect(0, y + 100, Screen.width, 50), "Teleport To Chest (F7): " + "Teleport to chest");
                GUI.Box(new Rect(0, y + 110, Screen.width, 50), "Activate Fly (F8): " + applyFly.ToString());
            } 
            GUI.Label(new Rect(50, 50, 200, 40), "Injected");
        }

        private void FetchAllObjects()
        {
            chestBehaviors = FindObjectsOfType<ChestBehavior>();
            networkUsers = FindObjectsOfType<NetworkUser>();
            master = FindObjectsOfType<CharacterMaster>();
        }

        private CharacterMaster GetLocalPlayerMaster(CharacterMaster[] characters) 
        {
            foreach(var character in characters)
            {
                if(character.name == "PlayerMaster(Clone)")
                {
                    return character;
                }
            }
            throw new Exception("Unable to find character");
        }

        private void CheckMenuInteraction()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (showConsole == true)
                {
                    showConsole = false;
                }
                else
                {
                    showConsole = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (applyGodmode == true)
                {
                    localPlayerBody.healthComponent.godMode = false;
                    applyGodmode = false;
                }
                else
                {
                    localPlayerBody.healthComponent.godMode = true;
                    applyGodmode = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                if (applySpeed == true)
                {
                    localPlayerBody.baseMoveSpeed = playerBaseMoveSpeed;
                    applySpeed = false;
                }
                else
                {
                    localPlayerBody.baseMoveSpeed = 20f;
                    applySpeed = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                if (applyDamageBoost == true)
                {
                    localPlayerBody.baseDamage = playerBaseDamage;
                    applyDamageBoost = false;
                }
                else
                {
                    localPlayerBody.baseDamage = 200f;
                    applyDamageBoost = true;
                }
            }

            if(Input.GetKeyDown(KeyCode.F5))
            {
                localPlayer.GiveMoney(1000);
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                if (scanForChests == true)
                { 
                    //Figure it out
                    scanForChests = false;
                }
                else
                {
                    //Figure it out
                    scanForChests = true;
                }
            }

            if(Input.GetKeyDown(KeyCode.F7))
            {
                TeleportToNearestChest(chestTransforms);
            }

            if(Input.GetKeyDown(KeyCode.F8))
            {
                applyFly = !applyFly;
            }
        }

        private void TeleportToNearestChest(List<Transform> chestTransforms)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (Transform t in chestTransforms)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            float playerHeight = localPlayerBody.transform.localScale.y / 2;
            TeleportHelper.TeleportBody(localPlayerBody, new Vector3(tMin.position.x, tMin.position.y + playerHeight, tMin.position.z));
        }
    }
}

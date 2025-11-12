using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ManualJoin : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private Dictionary<Gamepad, bool> deviceJoined = new Dictionary<Gamepad, bool>();
    private int playerCount = 0;

    void Start()
    {
        // ゲーム開始時に全ての接続済みコントローラーを確認
        foreach (var device in Gamepad.all)
        {
            if (!deviceJoined.ContainsKey(device))
                deviceJoined[device] = false;

            if (!deviceJoined[device])
            {
                if (playerCount == 0)
                {
                    PlayerInput.Instantiate(player1Prefab, pairWithDevice: device);
                    Debug.Log($"Player1 joined with {device.displayName}");
                }
                else if (playerCount == 1)
                {
                    PlayerInput.Instantiate(player2Prefab, pairWithDevice: device);
                    Debug.Log($"Player2 joined with {device.displayName}");
                }
                else
                {
                    Debug.Log("すでに2人まで参加しています。");
                    break;
                }

                deviceJoined[device] = true;
                playerCount++;
            }
        }
    }
}

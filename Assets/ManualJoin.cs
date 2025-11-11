using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ManualJoin : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private Dictionary<Gamepad, bool> deviceJoined = new Dictionary<Gamepad, bool>();
    private int playerCount = 0;

    void Update()
    {
        foreach (var device in Gamepad.all)
        {
            if (!deviceJoined.ContainsKey(device))
                deviceJoined[device] = false;

            // まだ生成されていないデバイスだけ処理
            if (!deviceJoined[device])
            {
                if (device.buttonSouth.wasPressedThisFrame && playerCount == 0)
                {
                    PlayerInput.Instantiate(player1Prefab, pairWithDevice: device);
                    deviceJoined[device] = true;
                    playerCount++;
                }
                else if (device.buttonEast.wasPressedThisFrame && playerCount == 1)
                {
                    PlayerInput.Instantiate(player2Prefab, pairWithDevice: device);
                    deviceJoined[device] = true;
                    playerCount++;
                }
            }
        }
    }
}

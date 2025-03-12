using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class LookAtUI : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(GameManager.instance.currentPlayer.followCam.transform);
        }
    }
}

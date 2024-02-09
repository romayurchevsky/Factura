using Scripts.CommonCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.LevelCode
{
    public class LevelEndZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameManager.StartResultAction.Invoke(true);
        }
    }
}

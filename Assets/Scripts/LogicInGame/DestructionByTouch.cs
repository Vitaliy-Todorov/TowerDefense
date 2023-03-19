using UnityEngine;

namespace Scrips._Test.Units
{
    public class DestructionByTouch : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
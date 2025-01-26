using UnityEngine;

namespace Enemies
{
    public class Thorn : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out IShootable shootable))
            {
                shootable.Shot();
            }
        }
    }
}
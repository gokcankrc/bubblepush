using Enemies;
using UnityEngine;

public class Bubble : MonoBehaviour, IShootable
{
    public void Shot(Projectile projectile, Shooter source)
    {
        Debug.Log("Bubble Shot");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class TransferShootable : MonoBehaviour, IShootable
{
   [SerializeField] private Component _shootable;

   public IShootable Shootable;
   private void Awake()
   {
       Shootable = _shootable.GetComponent<IShootable>();
   }

   public void Shot(Projectile projectile, Shooter source)
    {
       if (Shootable == null) return;
       Shootable.Shot(projectile, source);
    }
}

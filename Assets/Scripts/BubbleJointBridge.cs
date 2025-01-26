using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleJointBridge : MonoBehaviour
{
   public Bubble Bubble;
   public Rigidbody2D RigidBody;

   private void OnTriggerEnter2D(Collider2D other)
   {
      Bubble.BridgeTriggerEnter(other);
   }
   
   private void OnTriggerExit2D(Collider2D other)
   {
      Bubble.BridgeTriggerExit(other);
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      Bubble.BridgeCollisionEnter(other);
   }

   private void OnCollisionExit2D(Collision2D other)
   {
      Bubble.BridgeCollisionExit(other);
   }
}

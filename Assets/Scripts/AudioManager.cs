using System.Collections;
using Ky;
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
   public AudioSource MenuSound;
   public AudioSource GameSound;
   
   
   public void SwitchAudio(AudioSource from, AudioSource to, float duration)
   {
      StartCoroutine(SwitchCoroutine(from, to, duration));
   }

   private IEnumerator SwitchCoroutine(AudioSource from, AudioSource to, float duration)
   {
      var fromInitial = from.volume;
      var toInitial = to.volume;

      if (!from.isPlaying)
         from.Play();
      
      to.volume = 0f;
      
      if (!to.isPlaying) 
         to.Play();
      

      float t = 0f;

      while (t < duration)
      {
         t += Time.deltaTime;
         var ratio = t / duration;
         
         from.volume = Mathf.Lerp(fromInitial, 0, ratio);
         to.volume = Mathf.Lerp(0, toInitial, ratio);

         yield return null;
      }
      
      from.Stop();
      from.volume = fromInitial;
      to.volume = toInitial;
   }

   [Button]
   public void Play()
   {
      MenuSound.Play();
   }

   [Button]
   private void SwitchToGame()
   {
      SwitchAudio(MenuSound, GameSound, 2f);
   }

   [Button]
   private void SwitchToMenu()
   {
      SwitchAudio(GameSound, MenuSound, 2f);
   }
}

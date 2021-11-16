using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {

    public enum Sound {
        PlayerMove,
        PlayerJump,
        PlayerShoot,
        PlayerHit,
        EnemyMove,
        EnemyAttack,
        EnemyHit,
        EnemyDie

    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void Initialize(){
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0;
    }

    // Update is called once per frame

    public static void PlaySound(Sound sound, Vector3 position){
        if(CanPlaySound(sound)){
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound){
        if(CanPlaySound(sound)){
            if(oneShotGameObject == null){
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
            //Object.Destroy(soundGameObject, audioSource.clip.length);
            Debug.Log("bang");
        }
    }

    private static bool CanPlaySound(Sound sound){
        switch (sound) {
        default:
            return true;
        case Sound.PlayerMove:
            if(soundTimerDictionary.ContainsKey(sound)){
                float lastTimePlayed = soundTimerDictionary[sound];
                float playerMoveTimerMax = 0.5f;
                if(lastTimePlayed + playerMoveTimerMax < Time.time){
                    soundTimerDictionary[sound] = Time.time;
                    return true;
                } else {
                    return false;
                }
            } else {
                return true;
            }
        }
    }

    private static AudioClip GetAudioClip(Sound sound){
        foreach (GameController.SoundAudioClip soundAudioClip in GameController.instance.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound){
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}

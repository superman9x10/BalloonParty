using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    Dictionary<string, AudioClip> soundFxMap = new Dictionary<string, AudioClip>();
    [SerializeField] List<SoundFx> soundFxList;
    [SerializeField] AudioSource audio;

    private void OnEnable()
    {
        CharacterBase.hitBallon += playSoundFx;
        Balloon.balloonHitRangeWeapon += playSoundFx;
    }

    private void OnDisable()
    {
        CharacterBase.hitBallon -= playSoundFx;
        Balloon.balloonHitRangeWeapon -= playSoundFx;
    }
    private void Start()
    {
        foreach(SoundFx sound in soundFxList)
        {
            if(!soundFxMap.ContainsKey(sound.name))
            {
                soundFxMap.Add(sound.name, sound.audio);
            }
        }
    }


    void playSoundFx(string name, Vector3 pos)
    {
        audio.PlayOneShot(soundFxMap[name]);

    }

}

[System.Serializable]
public class SoundFx
{
    public string name;
    public AudioClip audio;
}
    

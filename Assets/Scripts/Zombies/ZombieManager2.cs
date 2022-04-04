using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ω© ¨π‹¿Ì
/// </summary>
public class ZombieManager2 : ZombieManager
{
    protected override IEnumerator GenerateZombies()
    {
        float zombieInterval = 20f;
        yield return new WaitForSeconds(zombieInterval);
        audioSource.clip = GameController.Instance.audioClipConf.zombieComingClip;
        audioSource.Play();
        CreateZombie(ZombieType.Zombie);
        yield return new WaitForSeconds(zombieInterval);
        CreateZombie(ZombieType.Zombie);
        yield return new WaitForSeconds(1f);
        CreateZombie(ZombieType.Zombie);
        yield return new WaitForSeconds(zombieInterval);
        CreateZombie(ZombieType.ConeheadZombie);
        yield return new WaitForSeconds(zombieInterval);
        CreateZombie(ZombieType.BucketheadZombie);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(zombieInterval);
            CreateRandomZombie();
            yield return new WaitForSeconds(1f);
            CreateRandomZombie();
            yield return new WaitForSeconds(1f);
            CreateRandomZombie();
        }
        yield return new WaitForSeconds(zombieInterval);

        GameObject finalWaveGO = GameController.Instance.transform.Find("FinalWave").gameObject;
        audioSource.clip = GameController.Instance.audioClipConf.hugeWaveClip;
        audioSource.Play();
        finalWaveGO.SetActive(true);
        finalWaveGO.GetComponent<Animator>().Play(0);
        yield return new WaitForSeconds(3f);
        finalWaveGO.SetActive(false);
        for (int i = 0; i < 9; i++)
        {
            CreateRandomZombie();
            yield return new WaitForSeconds(1f);
        }
        GameController.Instance.WinGame();
    }
}
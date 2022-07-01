using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeapon : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("BonusUI") != null)
        {
            
            if (BonusUI.instance.canThrow)
            {
                BonusUI.instance.canThrow = false;
                
                objectToThrow = gameObject.GetComponent<CharacterBase>().getWeapon();
                Throw(BonusUI.instance.throwForce);
            }
        }
        
    }

    private void Throw(float force)
    {
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, Quaternion.Euler(new Vector3(180, 0, 0)));
        projectile.AddComponent<BoxCollider>();
        projectile.AddComponent<Rigidbody>();
        projectile.GetComponent<BoxCollider>().isTrigger = true;
        projectile.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        projectile.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        StartCoroutine(startThrow(projectile, force));
        StartCoroutine(endGame());
        
    }
    IEnumerator startThrow(GameObject projectile, float force)
    {
        float startTime = Time.time;
        while (Time.time < startTime + 0.75f)
        {
            projectile.transform.position += force * Time.deltaTime * Vector3.forward;
            yield return null;
        }
        Destroy(projectile);
    }
    IEnumerator endGame()
    {
        yield return new WaitForSeconds(2f);
        CamController.instance.canChangeToBonusStageOffset = false;
        BonusUI.instance.isSetValue = false;
        GameManager.instance.updateGameState(GameManager.GameState.EndGame);
    }

}

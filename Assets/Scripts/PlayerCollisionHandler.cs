using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour {
    
    public void OnCollisionEnter(Collision other) { }

    public void OnCollisionExit(Collision other) { }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("UpsideDown"))
    //    {
    //        transform.Rotate(new Vector3(180, 0, 0), Space.Self);
    //    }

    //    if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        Debug.Log("Dead!");
    //        SceneManager.LoadScene(0);
    //    }
    //    else if (other.gameObject.layer == LayerMask.NameToLayer("EnemySpirit"))
    //    {
    //        Destroy(other.transform.parent.gameObject);
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("UpsideDown"))
    //    {
    //        GetComponent<PlayerController>().ToggleUpsideDownValues();
    //    }
    //}


}

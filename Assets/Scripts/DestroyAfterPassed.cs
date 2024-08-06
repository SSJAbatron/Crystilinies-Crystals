using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPassed : MonoBehaviour
{
    public GameObject areaRef;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(areaRef, 2f);
}
    }
}

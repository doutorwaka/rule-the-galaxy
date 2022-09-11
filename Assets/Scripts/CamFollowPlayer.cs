using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    private GameObject player;
    private BackgroundOffset[] bgsOffset;
    private float initialZ;
    // Start is called before the first frame update
    void Start()
    {
        initialZ = transform.position.z;
        player = GameObject.FindGameObjectWithTag("Player");
        bgsOffset = gameObject.GetComponentsInChildren<BackgroundOffset>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newCamPosition = player.transform.position;
        newCamPosition.z = initialZ;
        transform.position = newCamPosition;
        
        if(bgsOffset != null && bgsOffset.Length != 0){
            setNewOffsetToBackgroundChildrens(newCamPosition);
        }
    }

    private void setNewOffsetToBackgroundChildrens(Vector3 newOffset){
        foreach(BackgroundOffset bgo in bgsOffset){
            bgo.setNewOffset(newOffset);
        }
    }
}

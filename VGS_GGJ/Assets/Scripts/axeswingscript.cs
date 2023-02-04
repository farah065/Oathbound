using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeswingscript : MonoBehaviour
{
    float currangle = 0;
    bool directionright = true;
    float strength;
    public float dampener = 25;
    public float percdamp = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused)
        {
            if (currangle >= 0 && currangle < 150)
                strength = (Mathf.Abs(90 - currangle));
            else
                strength = Mathf.Abs(currangle - 360 + 90);
            if (directionright)
            {
                transform.eulerAngles += Vector3.forward * percdamp * strength / dampener;
                if (transform.eulerAngles.z >= 75 && transform.eulerAngles.z < 150)
                    directionright = false;
            }
            else
            {
                transform.eulerAngles += Vector3.back * percdamp * strength / dampener;
                if (transform.eulerAngles.z <= 285 && transform.eulerAngles.z > 210)
                    directionright = true;
            }

            currangle = transform.eulerAngles.z;
        }
    }
}

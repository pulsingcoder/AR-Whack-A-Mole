using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleMovementController : MonoBehaviour
{
    public GameObject[] moles;
    private float moleVisibleTime = 1.3f;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<moles.Length;i++)
        {
            moles[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        moleVisibleTime -= Time.fixedDeltaTime;
        if (moleVisibleTime < 0)
        {
            for (int i = 0; i < moles.Length; i++)
            {
                int random = Random.Range(-2, 2);
                if (random > 0)
                {
                    moles[i].SetActive(true);
                    count++;
                }
                else
                {
                    moles[i].SetActive(false);
                }

            }
            if (count <3)
            {
                moles[Random.Range(0, moles.Length - 1)].SetActive(true);
                moles[Random.Range(0, moles.Length - 1)].SetActive(true);
                moles[Random.Range(0, moles.Length - 1)].SetActive(true);
            }
            moleVisibleTime = 1.3f;
        }
    }
}

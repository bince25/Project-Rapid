using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TimerManager TimerManager;

    // Start is called before the first frame update
    void Start()
    {
        TimerManager.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimerManager.Instance.AddTime(10);
        }
    }


}

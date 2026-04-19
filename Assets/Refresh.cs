using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresh : MonoBehaviour
{
    public float RefreshDelay = 0.5f;
    public GameObject[] Columns;
    public GameObject[] Columns2;
    Coroutine Refresh1;
    bool hasRefreshed;
    bool fullyRefreshed;
    public int currentColumn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Columns = GameObject.FindGameObjectsWithTag("Column");
        Columns2 = GameObject.FindGameObjectsWithTag("Column2");
        foreach (GameObject column2 in Columns2)
        {
            column2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           hasRefreshed = !hasRefreshed;
           
                if (Refresh1 != null)
                {
                    StopCoroutine(Refresh1);
                    fullyRefreshed = false;
                    Debug.Log("Stopped midway");
                }
                Refresh1 = StartCoroutine(Refreshing());
            
            //else if (hasRefreshed)
            //{

            //}
        }
    }

    IEnumerator Refreshing()
    {
        while (currentColumn < Columns.Length)
        {
            if (hasRefreshed)
            {
                Columns[currentColumn].SetActive(!Columns[currentColumn].activeInHierarchy);
                Debug.Log("current column: " + currentColumn);
                currentColumn++;
            }
            else
            {
                //need to figure out how to stop at active column and reversing refresh
                if (Columns[currentColumn - 1].activeInHierarchy)
                {
                    Columns[currentColumn].SetActive(true);
                    Debug.Log("stop at column: " + currentColumn);
                    currentColumn = Mathf.Clamp(currentColumn, 0, 18);
                    yield break;
                }
                else
                {
                    Debug.Log("current column: " + currentColumn);
                    Columns[currentColumn].SetActive(!Columns[currentColumn].activeInHierarchy);
                    currentColumn--;
                }
            }
            yield return new WaitForSeconds(RefreshDelay);
            Debug.Log("run");
        }
        Debug.Log("done");
        fullyRefreshed = true;
        currentColumn = Mathf.Clamp(currentColumn, 0, 18);
        yield break;
    }
}

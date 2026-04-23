using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Columns = GameObject.FindGameObjectsWithTag("Column").OrderByDescending(o => 
        {
            var numberPart = new string(o.name.Where(char.IsDigit).ToArray());
            return int.Parse(numberPart);
        }).ToArray();

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
            Debug.Log("hasRefreshed" + hasRefreshed);
           
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
                Debug.Log("current column returning: " + currentColumn);
                currentColumn = Mathf.Clamp(currentColumn - 1, 0, 18);
                Columns[currentColumn].SetActive(!Columns[currentColumn].activeInHierarchy);

                if (currentColumn == 0)
                {
                    Debug.Log("done returning stopped at: " + currentColumn);
                    yield break;
                }
                //currentColumn--;

                //need to figure out how to stop at active column and reversing refresh
                //if (Columns[currentColumn].activeInHierarchy)
                //{
                //    Columns[currentColumn].SetActive(true);
                //    Debug.Log("stop at column: " + currentColumn);
                //    currentColumn = Mathf.Clamp(currentColumn, 0, 18);
                //    yield break;
                //}
                //else
                //{
                //    Debug.Log("current column: " + currentColumn);
                //    Columns[currentColumn].SetActive(!Columns[currentColumn].activeInHierarchy);
                //    currentColumn--;
                //}
            }
            yield return new WaitForSeconds(RefreshDelay);
            Debug.Log("run");
        }

        while (currentColumn > -1 & !hasRefreshed)
        {
            Debug.Log("current column returning: " + currentColumn);
            Columns[currentColumn].SetActive(!Columns[currentColumn].activeInHierarchy);
            currentColumn--;

            if (currentColumn < 0)
            {
                Debug.Log("done returning stopped at: " + currentColumn);
                currentColumn = 0;
                yield break;
            }

            yield return new WaitForSeconds(RefreshDelay);
            Debug.Log("run");
        }

        Debug.Log("done stopped at: " + currentColumn);
        fullyRefreshed = true;
        currentColumn = Mathf.Clamp(currentColumn, 0, 18);
        yield break;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Takumi_maze : MonoBehaviour
{


    GameObject block;
    int i = 0;
    float[][] a = {
                new float[]{ 0.2f,0.2f,0.1f,0.0f,0.2f,0.2f,0.1f,0.0f,0.3f,0.1f,0.2f,0.0f,0.3f,0.2f,0.2f,0.0f},
                new float[]{ 0.6f,0.6f,0.2f,0.0f,0.4f,0.5f,0.2f,0.0f,0.5f,0.4f,0.4f,0.0f,0.3f,0.3f,0.2f,0.0f},
                new float[]{ 0.7f,0.7f,0.3f,0.0f,0.6f,0.7f,0.4f,0.0f,0.6f,0.4f,0.5f,0.0f,0.4f,0.5f,0.3f,0.0f},
                new float[]{ 0.8f,0.8f,0.4f,0.0f,0.7f,0.7f,0.5f,0.0f,0.6f,0.4f,0.4f,0.0f,0.5f,0.5f,0.5f,0.0f},
                new float[]{ 0.9f,0.9f,0.4f,0.0f,0.9f,0.9f,0.6f,0.0f,0.7f,0.4f,0.4f,0.0f,0.6f,0.4f,0.4f,0.0f},
                new float[]{ 0.9f,0.9f,0.3f,0.0f,0.9f,0.9f,0.4f,0.0f,0.8f,0.3f,0.3f,0.0f,0.7f,0.3f,0.3f,0.0f},
                new float[]{ 0.9f,0.9f,0.3f,0.0f,0.9f,0.9f,0.4f,0.0f,0.8f,0.3f,0.3f,0.0f,0.8f,0.4f,0.3f,0.0f},
                new float[]{ 0.9f,0.9f,0.3f,0.0f,0.9f,0.9f,0.3f,0.0f,0.9f,0.3f,0.3f,0.0f,0.9f,0.3f,0.3f,0.0f},
                new float[]{ 0.9f,0.9f,0.3f,0.0f,0.9f,0.9f,0.3f,0.0f,0.9f,0.3f,0.2f,0.0f,0.9f,0.3f,0.2f,0.0f},
                new float[]{ 0.9f,0.9f,0.2f,0.0f,0.9f,0.9f,0.2f,0.0f,0.9f,0.2f,0.1f,0.0f,0.9f,0.2f,0.2f,0.0f},
                new float[]{ 1.0f,1.0f,0.2f,0.0f,1.0f,1.0f,0.2f,0.0f,1.0f,0.2f,0.1f,0.0f,1.0f,0.2f,0.2f,0.0f},
                new float[]{ 1.0f,1.0f,0.1f,0.0f,1.0f,1.0f,0.2f,0.0f,1.0f,0.2f,0.1f,0.0f,1.0f,0.1f,0.1f,0.0f},
                new float[]{ 1.0f,1.0f,0.1f,0.0f,1.0f,1.0f,0.1f,0.0f,1.0f,0.1f,0.1f,0.0f,1.0f,0.1f,0.1f,0.0f},
                new float[]{ 1.0f,1.0f,0.1f,0.0f,1.0f,1.0f,0.1f,0.0f,1.0f,0.1f,0.1f,0.0f,1.0f,0.1f,0.1f,0.0f},
                new float[]{ 1.0f,1.0f,0.1f,0.0f,1.0f,1.0f,0.1f,0.0f,1.0f,0.1f,0.1f,0.0f,1.0f,0.1f,0.1f,0.0f}
                };
    // Use this for initialization
    void Start()
    {
                


    }



void Update()
    {
        int n;
        for(int i = 0; i< 100000000; i++)
        {
            n = 1;
        }
        block = GameObject.Find("0");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][0], 1.0f, 1.0f));
        block = GameObject.Find("1");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][1], 1.0f, 1.0f));
        block = GameObject.Find("2");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][2], 1.0f, 1.0f));
        block = GameObject.Find("3");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][3], 1.0f, 1.0f));
        block = GameObject.Find("4");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][4], 1.0f, 1.0f));
        block = GameObject.Find("5");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][5], 1.0f, 1.0f));
        block = GameObject.Find("6");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][6], 1.0f, 1.0f));
        block = GameObject.Find("7");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][7], 1.0f, 1.0f));
        block = GameObject.Find("8");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][8], 1.0f, 1.0f));
        block = GameObject.Find("9");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][9], 1.0f, 1.0f));
        block = GameObject.Find("10");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][10], 1.0f, 1.0f));
        block = GameObject.Find("11");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][11], 1.0f, 1.0f));
        block = GameObject.Find("12");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][12], 1.0f, 1.0f));
        block = GameObject.Find("13");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][13], 1.0f, 1.0f));
        block = GameObject.Find("14");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][14], 1.0f, 1.0f));
        block = GameObject.Find("15");
        block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1-a[i][15], 1.0f, 1.0f));
        if(i <= 13)
        {
            i++;
        }


    }
}


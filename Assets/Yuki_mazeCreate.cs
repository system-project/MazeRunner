using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Yuki_mazeCreate : MonoBehaviour {

    public GameObject blockPrefab;
	public int count = 8;
	int num = 0;//idに入れるためのやつ

    public int[][] information;
    //= {
    //    new int[]{ 0, 1 },
    //    new int[]{ 1, 2 },
    //    new int[]{ 2, 3 },
    //    new int[]{ 2, 6 },
    //    new int[]{ 1, 5 },
    //    new int[]{ 4, 5 },
    //    new int[]{ 5, 6 },
    //    new int[]{ 5, 9 },
    //    new int[]{ 6, 7 },
    //    new int[]{ 4, 8 },
    //    new int[]{ 8, 12 },
    //    new int[]{ 6, 10 },
    //    new int[]{ 9, 10 },
    //    new int[]{ 10, 14 },
    //    new int[]{ 12, 13 },
    //    new int[]{ 13, 14 },
    //    new int[]{ 14, 15 },
    //    new int[]{ 15, 11 }
    //};

    public int[][] Marshal(int count)
    {
        int t = 2 * (count - 1) * count;
        int[][] info = new int[t][];
        int z = 0;

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                int n = count * j + i;
                if (i == count - 1 && j == count - 1)
                {

                }
                else if (i == count - 1)
                {//右無し
                    info[z] = new int[] { n, n + count };
                }
                else if (j == count - 1)
                {//下なし
                    info[z] = new int[] { n, n + 1 };
                }
                else
                {
                    info[z] = new int[] { n, n + count };
                    info[++z] = new int[] { n, n + 1 };
                }
                z++;
            }
        }

        var tempList = new List<int[]>();
        tempList.AddRange(info);

        int num = 0;

        for (int a = 0; a < count*3; a++)
        {
            int random = (int)Random.Range(0f, (float)t - num-1);
            tempList.RemoveAt(random);
            num++;
        }


        return tempList.ToArray();
    }



public List<GameObject> list = new List<GameObject>();

    void Awake () {
        information = Marshal(count);


        Color black = new Color(0, 0, 0);

        var list = new List<GameObject>();

		for (int i=0; i<count*2+1; i++)
		{
            for(int j=0; j<count*2+1; j++)
            {
				var x = j % (count*2+1);
				var z = i;
				var block = Instantiate(blockPrefab, new Vector3(-1*(float)x, 0.0f, (float)z), Quaternion.identity);
                list.Add(block);
				//block.name = count.ToString();
				var script = block.GetComponent<BlockScriptForYuki>();
				if (i % 2 == 1 && j % 2 == 1)
                {
					script.id = num;
                    block.name = num.ToString();
                    num++;
                }
                else
                {
					block.GetComponent<Renderer>().material.SetColor("_Color", black);
				}
                script.id = j+i*(count*2+1);
            }
		}

        foreach(int[] bond in information)
        {
            int diff = bond.Max() - bond.Min();//1だったら右 4だったら下
            GameObject branch = GameObject.Find(bond.Min().ToString());
            var script = branch.GetComponent<BlockScriptForYuki>();
            if(diff==1)//右壁
            {
                list[script.id + 1].GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1));
            } else if(diff==count)//下壁
            {
                list[script.id + (count*2+1)].GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1));
            }
        }



    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

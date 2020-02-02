using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControllScriptForYuki : MonoBehaviour {
	public GameObject blockPrefab;
	// Use this for initialization
	int count = 0;
	void Awake() {
        for (; count < 16; count++)
        {
			var x = count % 4 * 1.25;
			var z = count / 4 * 1.25;
			var block = Instantiate(blockPrefab, new Vector3((float)x, 0.0f, (float)z), Quaternion.identity);
			block.name = count.ToString();
			var script = block.GetComponent<BlockScriptForYuki>();
			script.id = count;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

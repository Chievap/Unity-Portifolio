using UnityEngine;
using System.Collections;

public class MapMovement : MonoBehaviour {
	public float moveSpeed;
	public GameObject mapGen;

	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetAxis("Horizontal") > 0){
			mapGen.GetComponent<MapGenerator>().offset.x -= moveSpeed;
            UpdateGen();    
        } else if(Input.GetAxis("Horizontal") < 0){
			mapGen.GetComponent<MapGenerator>().offset.x += moveSpeed;
            UpdateGen();
        }

		if(Input.GetAxis("Vertical") > 0){
			mapGen.GetComponent<MapGenerator>().offset.y -= moveSpeed;
            UpdateGen();
        } else if(Input.GetAxis("Vertical") < 0){
				mapGen.GetComponent<MapGenerator>().offset.y += moveSpeed;
            UpdateGen();

        }

        
    }

    public void UpdateGen() {
        mapGen.GetComponent<MapGenerator>().GenerateMap();
    }
}

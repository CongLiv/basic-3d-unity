using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class scriptSpawner : MonoBehaviour
{

    int tmpPoint = 0;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        int currentPoint = player.GetComponent<Controller>().getPoint();
        int currentLevel = player.GetComponent<Controller>().getLevel();
        // /print(currentPoint);
        if (tmpPoint < currentPoint)
        {
            Color color = player.GetComponent<MeshRenderer>().material.color;
            GameObject gObj = GameObject.FindGameObjectWithTag("Sample");
            GameObject clone = Instantiate(gObj);
            clone.GetComponent<MeshRenderer>().material.color = color;
            clone.GetComponent<Rigidbody>().useGravity = true;
            clone.tag = "Hit Point";
            float randX = UnityEngine.Random.Range(-9, 9);
            float randZ = UnityEngine.Random.Range(-9, 9);
            
            if (currentLevel == 3){
                float tmp1 = UnityEngine.Random.Range(-20, -18);
                float tmp2 = UnityEngine.Random.Range(18, 20);

                if (Mathf.Abs(tmp1) > Mathf.Abs(tmp2)) randX = tmp1;
                else randX = tmp2;
            }

            Vector3 randomPos = new Vector3(randX, 20, randZ);
            Instantiate(clone, randomPos, Quaternion.identity);
            player.GetComponent<MeshRenderer>().material.color = chooseColor();
            tmpPoint++;
        }
    }

    Color chooseColor()
    {
        Color color;

        System.Random rd = new System.Random();
        int num = rd.Next();
        if (num % 3 == 0) color = Color.red;
        else if (num % 3 == 1) color = Color.green;
        else color = Color.blue;
        return color;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] float minCarSpawnInterval;
    [SerializeField] float maxCarSpawnInterval;
    [SerializeField] Car carPrefab;

    float timer;

    Vector3 carSpawnPosition;
    Quaternion carRotation;

    private void Start()
    {
        if (Random.value > 0.5f)
        {
        
            carSpawnPosition = new Vector3(
                                           horizontalSize / 2 + 6,
                                           0,
                                           this.transform.position.z);

            carRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
 
            carSpawnPosition = new Vector3(
                                           -(horizontalSize / 2 + 6),
                                           0,
                                           this.transform.position.z);

            carRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Update()
    { 
        if(timer <= 0)
        {
            timer = Random.Range(
                minCarSpawnInterval, 
                maxCarSpawnInterval);

            var car = Instantiate(carPrefab,
                                  carSpawnPosition,
                                  carRotation);

            car.SetUpDistanceLimit(horizontalSize + 12);
            
            //spawn
            return;
        }

        timer -= Time.deltaTime;
    }
}

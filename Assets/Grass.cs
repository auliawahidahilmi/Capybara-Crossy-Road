using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{
    [SerializeField] List<GameObject> treePrefabList;
    [SerializeField, Range(0, 1)] float treeProbability;

    public void SetTreePercentage(float newProbability)
    {
        this.treeProbability = Mathf.Clamp01(newProbability);
    }

    public override void Generate(int size)
    {
        base.Generate(size);

        //bikin pohon
        var limit = Mathf.FloorToInt((float)size / 2);
        var treeCount = Mathf.FloorToInt((float)size * treeProbability);
        
        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);
        }
        for (int i = 0; i < treeCount; i++)
        {
            //milih posisi kosong secara random
            var randomIndex = Random.Range(0, emptyPosition.Count);
            var pos = emptyPosition[randomIndex];
            
            //agar ngga kepilih angka yang sama, bakal remove angka yg kepilih, biar pohon ngga numbuh di tempat yg sama
            emptyPosition.RemoveAt(randomIndex);

            SpawnRandomTree(pos);
        }

        //bikin pohon ada di ujung kiri - kanan +
        SpawnRandomTree(-limit - 1);
        SpawnRandomTree(limit + 1);
    }

    private void SpawnRandomTree(int xPos)
    {
            //milih prefabs tree secara random
            var randomIndex = Random.Range(0, treePrefabList.Count);
            var prefab = treePrefabList[randomIndex];

            //set pohon ke posisi yang terpilih
            var tree = Instantiate(
                prefab,
                new Vector3(xPos, 0, this.transform.position.z),
                Quaternion.identity,
                transform);          

    }
}

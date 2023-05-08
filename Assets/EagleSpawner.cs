using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    [SerializeField] Eagle eagle;

    [SerializeField] Capybara capybara;

    [SerializeField] float initialTimer = 10;

    float timer;

    private void Start()
    {
        timer = initialTimer;
        eagle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timer <= 0 && eagle.gameObject.activeInHierarchy == false)
        {
            eagle.gameObject.SetActive(true);
            eagle.transform.position = capybara.transform.position + new Vector3(0, 0, 13);
            capybara.SetMoveAble(false);
        }

        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = initialTimer;
    }
}

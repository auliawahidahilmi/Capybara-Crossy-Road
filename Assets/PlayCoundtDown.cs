using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class PlayCoundtDown : MonoBehaviour
{

    [SerializeField] TMP_Text tmpText;

    [SerializeField] AudioSource countDownSound;

    public UnityEvent OnStart;

    public UnityEvent OnEnd;

    private void Start()
    {
        OnStart.Invoke();
        var sequence = DOTween.Sequence();
        tmpText.transform.localScale = Vector3.zero;
        countDownSound.Play();
        sequence.Append(tmpText.transform.DOScale(
            Vector3.one,
            1).OnComplete(() =>
                    {
                        tmpText.transform.localPosition = Vector3.zero;
                        tmpText.text = "3";
                    }));

        sequence.Append(tmpText.transform.DOScale(
           Vector3.one,
           1).OnComplete(() =>
           {
               tmpText.transform.localPosition = Vector3.zero;
               tmpText.text = "2";
           }));

        sequence.Append(tmpText.transform.DOScale(
           Vector3.one,
           1).OnComplete(() =>
           {
               tmpText.transform.localPosition = Vector3.zero;
               tmpText.text = "1";
           }));

        sequence.Append(tmpText.transform.DOScale(
           Vector3.one,
           1).OnComplete(() =>
           {
               tmpText.transform.localPosition = Vector3.zero;
               tmpText.text = "GO!";
           }));

        sequence.Append(tmpText.transform.DOScale(
           Vector3.one,
           1).OnComplete(() =>
           {
               OnEnd.Invoke();
           }));
    }
}

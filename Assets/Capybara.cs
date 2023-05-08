using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Capybara : MonoBehaviour
{
    [SerializeField, Range(0,1)] float moveDuration = 0.1f;
    [SerializeField, Range(0,1)] float jumpHeight = 0.5f;

    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;

    [SerializeField] AudioSource splatSound;
    [SerializeField] AudioSource eagleSound;
    [SerializeField] AudioSource gameOverSound;
    [SerializeField] AudioSource coinSound;

    //[SerializeField] UnityEvent onGetCoinSFX;
    //[SerializeField] UnityEvent onGetEagleSFX;
    [SerializeField] UnityEvent bgm;


    public bool alreadyPlayed = false;
    public AudioClip eagleClip;
    //public AudioClip gameOverClip;
    

    public UnityEvent<Vector3> OnJumpEnd;

    public UnityEvent<int> onGetCoin;

    public UnityEvent OnDie;

    public UnityEvent OnCarCollision;

    private bool isMoveable = false;

    void Update()
    {
        if (isMoveable == false)
            return;

        if (DOTween.IsTweening(transform))
            return;            

        Vector3 direction = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector3.forward; 
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }

        if (direction == Vector3.zero)
            return;
        Move(direction);
    }

    public void Move(Vector3 direction)
    {
        var targetPosition = transform.position + direction;
        //biar ga bisa nembus pohon
        if (targetPosition.x < leftMoveLimit ||
            targetPosition.x > rightMoveLimit ||
            targetPosition.z < backMoveLimit ||
            Treex.AllPositions.Contains(targetPosition)) 
        {

            targetPosition = transform.position;
        } 

        transform.DOJump(targetPosition,
                         jumpHeight,
                         1,
                         moveDuration)
            .onComplete = BroadCastPositionOnJumpEnd;

        transform.forward = direction;
    }

    public void SetMoveAble(bool value)
    {
        isMoveable = value;
    }

    public void UpdateMoveLimit(int horizontalSizeLimit, int backLimit)
    {
        leftMoveLimit = -horizontalSizeLimit / 2;
        rightMoveLimit = horizontalSizeLimit / 2;
        backMoveLimit = backLimit;
    }
    private void BroadCastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (transform.localScale.y == 0.1f)
                return;

            transform.DOScale(new Vector3(2,0.1f,2), 0.2f);
            splatSound.Play();

            isMoveable = false;
            OnCarCollision.Invoke();
            Invoke("Die", 3);
        }
        else if (other.CompareTag("Coin"))
        {
            var coin = other.GetComponent<Coin>();

            //onGetCoinSFX.Invoke();
            coinSound.Play();

            onGetCoin.Invoke(coin.Value);
            coin.Collected();
        }

        else if (other.CompareTag("Eagle"))
        {
            if (this.transform != other.transform)
            {   
               
                this.transform.SetParent(other.transform);
                if (!alreadyPlayed)
                {
                    eagleSound.PlayOneShot(eagleClip);
                    alreadyPlayed = true;
                }
                //onGetEagleSFX.Invoke();
                Invoke("Die", 3);               

            }
        }

    }

    private void Die()
    {
        
        //gameOverSound.Play();
        OnDie.Invoke();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [SerializeField] Transform fish;

    float fishPosition;
    float fishDestination;
    float fishTimer;
    [SerializeField] float timerMultiplicator = 3f;

    float fishSpeed;
    [SerializeField] float smoothMotion = 1f;

    [SerializeField] Transform hook;
    float hookPosition;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float hookPower = 5f;
    float hookProgress;
    float hookPullVelocity;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravityPower = 0.005f;
    [SerializeField] float hookProgressDegredationPower = 0.1f;

    [SerializeField] SpriteRenderer hookSpriteRenderer;

    [SerializeField] Transform progressBarContent;

    [SerializeField] float failTimer = 10f;

    bool pause = false;

    private void Start(){
        gameObject.SetActive(false);
        Resize();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause) { return; }
        Fish();
        Hook();
        ProgressCheck();
    }

    private void Resize(){
        Bounds b = hookSpriteRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.y = (distance / ySize * hookSize);
        hook.localScale = ls;
    }

    private void ProgressCheck(){
        Vector3 ls = progressBarContent.localScale;
        ls.y = hookProgress;
        progressBarContent.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;
        if (min < fishPosition && fishPosition < max){
            hookProgress += hookPower * Time.deltaTime;
        }
        else{
            hookProgress -= hookProgressDegredationPower * Time.deltaTime;

            failTimer -= Time.deltaTime;
            if(failTimer < 0f){
                Lose();
            }
        }

        if(hookProgress >= 1f){
            Win();
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }

    private void Lose(){
        pause = true;
        Debug.Log("You lost the fish!");
    }

    private void Win(){
        pause = true;
        Debug.Log("You caught the fish!");
    }

    void Hook(){
        if(Input.GetMouseButton(0)){
            hookPullVelocity += (hookPullPower) * Time.deltaTime;
        }

        hookPullVelocity -= (hookGravityPower) * Time.deltaTime;

        hookPosition += hookPullVelocity;

        if (hookPosition - hookSize / 2 <= 0f && hookPullVelocity < 0f){
            hookPullVelocity = 0f;
        }
        if(hookPosition + hookSize / 2 >= 1f && hookPullVelocity > 0f){
            hookPullVelocity = 0f;
        }

        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);
    }

    void Fish(){
        fishTimer -= Time.deltaTime;
            if (fishTimer < 0f){
                fishTimer= UnityEngine.Random.value * timerMultiplicator;

                fishDestination = UnityEngine.Random.value;
            }

            fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);

            fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }
}

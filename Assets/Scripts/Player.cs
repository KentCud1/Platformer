﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Controller2d))]
public class Player : MonoBehaviour {

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 3;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .6f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    public Vector2 jumpBack;

    Controller2d controller;

    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;

    void Start() {
        controller = GetComponent<Controller2d>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
    }

    private void Update() {

        CalculateVelocity();
        HandleWallSliding();

        if(Input.GetKeyDown(KeyCode.I) && !controller.collisions.knockedBack) {
            controller.Knockback();

        }

        if(controller.collisions.knockedBack && !controller.collisions.oldKnockBack) {
            velocity.x = -controller.collisions.faceDir * jumpBack.x;
            velocity.y = jumpBack.y;
        }


        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.below && controller.collisions.knockedBack && controller.collisions.oldKnockBack) {
            velocity.x = 0;
            controller.collisions.knockedBack = false;
        }

        if (controller.collisions.above || controller.collisions.below) {
            if (controller.collisions.slidingDownMaxSlope) {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else {
                velocity.y = 0;
            }
        }
    }

    public void SetDirectionalInput(Vector2 input) {
        directionalInput = input;
    }

    public void OnJumpInputDown() {
        if (wallSliding) {
            if (wallDirX == directionalInput.x) {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0) {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
        if (controller.collisions.below) {
            if (controller.collisions.slidingDownMaxSlope) {
                if(directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x)) {
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.x;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;

                }
            }
            else {
                velocity.y = maxJumpVelocity;
            }
        }
    }

    public void OnJumpInputUp() {
        if (velocity.y > minJumpVelocity) {
            velocity.y = minJumpVelocity;
        }
    }

    void HandleWallSliding() {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax) {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0) {

                velocityXSmoothing = 0;
                velocity.x = 0;


                if (directionalInput.x != wallDirX && directionalInput.x != 0) {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    void CalculateVelocity() {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

}
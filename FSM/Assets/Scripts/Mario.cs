using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : Agent {

    public int score = 0;

    protected override void FiniteStateMachine()
    {
        switch(state) {
            case 0:
                MoveTo(GetClosestCoin());
                if(isBooClose()){
                    state = 1;
                }
                break;
            case 1:
            //Should flee from the Boos
            MoveTo(GetClosestCoin());
            Debug.Log("Boo is too close");
                if(!isBooClose()){
                    state = 0;
                }
                break;
        }
    }

    Transform GetClosestCoin()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        float minDistance = float.PositiveInfinity;
        GameObject closestCoin = null;
        foreach (GameObject coin in coins)
        {
            float distance = Vector3.Distance(coin.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCoin = coin;
            }
        }
        return closestCoin.transform;
    }

    private bool isBooClose() {
        GameObject[] boos = GameObject.FindGameObjectsWithTag("Boo");
        float scareDistance = 2;
        foreach (GameObject boo in boos)
        {
            float distance = Vector3.Distance(boo.transform.position, transform.position);
            if (distance < scareDistance)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            score++;
            Debug.Log("Got a coin!");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Boo")
        {
            Debug.Log("Mamma mia! [score:" + score + "]");
            Destroy(gameObject);
        }
    }
}

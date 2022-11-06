using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HeneGames.Airplane
{
    public class SimpleAirPlaneCollider : MonoBehaviour
    {
        public bool collideSometing;
        public List<GameObject> towersList = new List<GameObject>();
        public List<GameObject> coinsList = new List<GameObject>();
        public GameManagement gameManagement;

        private void Start()
        {
            gameManagement = FindObjectOfType<GameManagement>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Walls" || other.tag == "Wall")
            {
                collideSometing = true;
                gameManagement.instance.gameOver();
            }
            else if (other.tag == "Coin" || other.tag == "Red Diamond" || other.tag == "Blue Diamond")
            {
                if (other.tag == "Coin")
                    gameManagement.instance.IncreaseScore(10);
                else if (other.tag == "Red Diamond")
                    gameManagement.instance.IncreaseScore(15);
                else if (other.tag == "Blue Diamond")
                    gameManagement.instance.IncreaseScore(20);
                else if (other.tag == "Green Diamond")
                    gameManagement.instance.IncreaseScore(20);
                other.gameObject.SetActive(false);
                coinsList.Add(other.gameObject);
                Invoke("recreateCoin", 5f);
            }
        }
        void recreateCoin()
        {
            coinsList[0].SetActive(true);
            coinsList.RemoveAt(0);
        }
        void DestroyTower()
        {
            Destroy(towersList[0]);
            towersList.RemoveAt(0);
        }
    }
}
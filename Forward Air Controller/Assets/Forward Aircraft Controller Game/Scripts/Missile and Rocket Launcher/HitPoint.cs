using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Missiles
{
    public class HitPoint : MonoBehaviour
    {
        public float maxHitPoint = 100;
        public float hitPoint = 0;

        public GameObject PointBars;
        //public Image CurrentHitPoint;

        private float hitRatio;
        void Start()
        {

            hitPoint = maxHitPoint;

            if (PointBars != null)
            {
                PointBars.GetComponent<Text>().text = hitPoint.ToString();
            }
        }

        public void UpdatePointsBars()
        {
            hitRatio = hitPoint / maxHitPoint;

            if (PointBars != null)
            {
                PointBars.GetComponent<Text>().text = hitPoint.ToString();

                if (hitPoint < 0f)
                {
                    PointBars.GetComponent<Text>().color = Color.red;
                }
            }
            //CurrentHitPoint.rectTransform.localScale = new Vector3(hitRatio, 1, 1);
        }

        public void ApplyDamage(float amount)
        {
            //Reduce the amount of health for the attached component
            hitPoint -= amount;

            //Debug.Log("HitPoint :" + GetComponent<DisplayDamage>().CurrentDamage.ToString());


            UpdatePointsBars();
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Dead();
            }
        }


        void Dead()
        {
            Enemy enemyController = GetComponent<Enemy>();
            if (enemyController)
            {
                enemyController.Dead();
            }
        }
    }
}

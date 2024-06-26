using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DottedLine
{
    public class DottedLine: MonoBehaviour
    {
        // Inspector fields
        public Sprite Dot;
        [Range(0.01f, 1f)]
        public float Size;
        [Range(0.1f, 2f)]
        public float Delta;
        [Range(0, 0.1f)]
        public float alpha;

        //Static Property with backing field
        private static DottedLine instance;
        public static DottedLine Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<DottedLine>();
                return instance;
            }
        }

        //Utility fields
       public List<Vector2> positions = new List<Vector2>();
       public List<GameObject> dots = new List<GameObject>();

        // Update is called once per frame
        void FixedUpdate()
        {
            if (positions.Count > 0)
            {
                DestroyAllDots();
                positions.Clear();
            }

        }

        private void DestroyAllDots()
        {
            foreach (var dot in dots)
            {
              //  Debug.Log("Nhay vao day: ");
                if (dot != null)
                {
                    dot.SetActive(false);
                }

               // Destroy(dot);
            }

            dots.Clear();
        }

        GameObject GetOneDot()
        {

            // GameObject ball = BallPool.SharedInstance.GetPooledObject();
            GameObject ball = ObjectPools.SharedInstance.GetObjectFromPool(0);
            if (ball != null)
            {
                ball.transform.localScale = Vector3.one * Size;
               // ball.transform.parent = transform;
                return ball;
            }

            return null;

        }
        public float dotSpacing = 0.1f;
        public void DrawDottedLine(Vector2 start, Vector2 end)
        {
            DestroyAllDots();
            try
            {
                Vector2 point = start;
                Vector2 direction = (end - start).normalized;
                // Debug.Log("distance ========> " + Vector3.Distance(start, end));
                //while ((end - start).magnitude > (point - start).magnitude)
                while ((end - start).magnitude > (point - start).magnitude)
                {
                    positions.Add(point);
                    point += (direction * Delta);
                }
            }catch(Exception e)
            {
               // Debug.Log("distance Exception ========> " + e);
            }
            Render();
         
        }

        private void Render()
        {
            foreach (var position in positions)
            {      
                var g = GetOneDot();
                if (g != null)
                {
                    g.transform.position = position;
                    dots.Add(g);
                 
                }
            }
        }
    }
}
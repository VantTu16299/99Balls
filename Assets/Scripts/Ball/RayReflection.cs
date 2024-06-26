using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace rayCast
{
    public class RayReflection : MonoBehaviour
    {
        public Transform laserSpawner;
        public LayerMask _layerMask;
        public LineRenderer line;
        //[SerializeField] Vector2 minMaxAngle;

        public float maxRaycastDistance = 10f; // Độ dài tối đa của tia ray

        void Start()
        {
            if (gameObject.GetComponent<LineRenderer>() != null)
            {
                line = gameObject.GetComponent<LineRenderer>();

            }

            //for (int i = 0; i < line.positionCount; i++)
            //{
            //    line.SetPosition(i, new Vector3(laserSpawner.position.x, laserSpawner.position.y, 0));
            //}
        }

        void FixedUpdate()
        {

            if (Input.GetMouseButton(0))
            {
                line.enabled = true;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 v2 = new Vector3((Camera.main.ScreenToWorldPoint(Input.mousePosition) - 1 * laserSpawner.position).x, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - 1 * laserSpawner.position).y, 0);
                Vector3 def = line.transform.position;

               // line.positionCount = 3;
                //line.SetPosition(0, new Vector3(def.x, def.y, -1f));
               // line.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, -1f));

                 DrawLine(laserSpawner.position, v2, 1);

             

            }
            else
            {
                for (int i = 0; i < line.positionCount; i++)
                {
                    line.SetPosition(i, new Vector3(laserSpawner.position.x, laserSpawner.position.y, 0));
                }
                line.enabled = false;
            }


        }

   
        private Vector2 direction;
        private float angle;
        public float angleMin;
        public float angleMax;
        void DrawLine(Vector2 initRayPos, Vector2 lastRayPos, int linePosIndex)
        {
           // Debug.Log("line.positionCount: " + line.positionCount);
            if (linePosIndex < line.positionCount)
            {
           
                RaycastHit2D hit = Physics2D.Raycast(initRayPos, lastRayPos - initRayPos, Vector3.Distance(lastRayPos, initRayPos), _layerMask);
               
                Vector2 endPoint = lastRayPos;

                if (hit.collider != null)
                {
                 
                    endPoint = hit.point;
                    float remDist = Vector3.Distance(lastRayPos, initRayPos) - hit.distance;

                    Vector2 refVect = remDist * Vector2.Reflect((hit.point - initRayPos), hit.normal).normalized;
                    DrawLine(hit.point + new Vector2(refVect.x * 0.01f, refVect.y * 0.01f), refVect + hit.point, linePosIndex + 1);
                    Debug.DrawLine(hit.point, hit.point + refVect.normalized * 2f, Color.blue);
                    //  DottedLine.DottedLine.Instance.DrawDottedLine(hit.point, hit.point + refVect.normalized * 2f);
                   // line.SetPosition(2, hit.point);
                    //line.SetPosition(3, hit.point + refVect.normalized * 2f);

                }
                //line.SetPosition(0, initRayPos);
                //line.SetPosition(1, endPoint);

                DottedLine.DottedLine.Instance.DrawDottedLine(initRayPos, endPoint);
                direction = (endPoint - initRayPos).normalized;
                Debug.DrawRay(initRayPos, lastRayPos - initRayPos);
                //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                //Vector3 dir = Input.mousePosition - pos;
                //angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //if (angle >= angleMin && angle <= angleMax)
                //{
                //   DottedLine.DottedLine.Instance.DrawDottedLine(initRayPos, endPoint);
                //   direction = (endPoint - initRayPos).normalized;
                //   Debug.DrawRay(initRayPos, lastRayPos - initRayPos);

                //}
                //else
                //{
                //    if (gameObject.activeSelf == true)
                //    {
                //       // gameObject.SetActive(false);
                //    }
                //}
              
            }

        }

    

        public GameObject ballPrefab;
        // public GameObject FirstBall;

        public GameObject canvas;

        //private bool stop = true;

        public GameObject gameObjectFirst;

    
        public void Update()
        {

            if (Input.GetMouseButtonUp(0))
            {
                if (Data.checkShoot == true)
                {
                    StartCoroutine(ShootBalls());
                }
                
              
            }

        }

        public IEnumerator ShootBalls()
        {
            Data.checkShoot = false;
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject ball = ObjectPools.SharedInstance.GetObjectFromPool(5);

                if (ball != null)
                {
                    if (i == 0)
                    {

                        GameData.boolFisrtBall = true;
                    }
                    ball.transform.position = transform.position;
                    ball.SetActive(true);
                    ball.transform.SetParent(canvas.transform, true);

                    // ball.GetComponent<Rigidbody2D>().AddForce(direction * 500f); // Sử dụng hướng tính được từ đường line

                    Vector2 force = direction * 300f;
                    ball.GetComponent<Rigidbody2D>().AddForce(force);

                    //if (ball.GetComponent<Rigidbody2D>().velocity.magnitude > 200f)
                    //{
                    //    // Đặt lại tốc độ về tốc độ tối đa với cùng hướng
                    //    ball.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Rigidbody2D>().velocity.normalized * 500f;
                    //}

                    // Thêm lực vào quả bóng
                 

                }
               
            }

            gameObjectFirst.SetActive(false);
        }

        public void ResetBalls()
        {
            //Wall.firstHit = true;
            //stop = true;
            //foreach (GameObject go in list)
            //{
            //    if (go != null)
            //    {
            //        go.GetComponent<CircleCollider2D>().enabled = false;
            //        go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //        go.GetComponent<BallMoveto>().Move = true;
            //    }
            //}
            //tmp.text = BallCount+"x";
        }
    }
}
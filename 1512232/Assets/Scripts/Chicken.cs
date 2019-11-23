using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public GameObject player;
    public bool follow = false;
    //private Kitty _kitty;
    // Start is called before the first frame update
    void Start()
    {
        //_kitty = player.GetComponent<Kitty>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!follow)
            return;
        if (!player.GetComponent<SpriteRenderer>().flipX)
        {
            //kitty di chuyen ben phai
            this.gameObject.transform.position = Vector2.MoveTowards
               (
               new Vector3(transform.position.x, transform.position.y, transform.position.z),
               new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z),
               6f * Time.deltaTime
               );
        }
        else
        {
            //kitty di chuyen ben trai
            this.gameObject.transform.position = Vector2.MoveTowards
               (
               new Vector3(transform.position.x, transform.position.y, transform.position.z),
               new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z),
               6f * Time.deltaTime
               );
        }
       
       
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Nếu đã có pet chicken từ trước
            int children = player.transform.childCount;
            if (children == 2)
                return;
            follow = true;
            transform.SetParent(player.transform);
        }
        if(col.tag == "Max")
        {
            follow = false;
            transform.parent=null;
        }
    }
}

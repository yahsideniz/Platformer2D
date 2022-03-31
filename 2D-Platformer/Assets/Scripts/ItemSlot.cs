using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    
    public  Item item;
    public  bool isEmpty;
    public  Image itemImage;

     void Start()
    {
        isEmpty = true;
        itemImage.enabled = false;
    }


    public  void SetItem(Item i)
    {
        item = i;
        isEmpty = false;
        itemImage.enabled = true;
        itemImage.sprite = item.ItemSprite;
        
    }

    public void UseItem()
    {
        if(item == null)
        {
            return;
        }

        Invoke(item.ItemName,0);
        item = null;
        isEmpty = true;
        itemImage.enabled = false;
    }

    public void Cherry()
    {
        GameObject.Find("Player").GetComponent<Player>().AddLife(); // ismi player olan objeye ulaþýp, istediðimiz fonk. çaðýrdýk
    }
}

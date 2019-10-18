using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPicker : MonoBehaviour
{
    [SerializeField]
    protected float rotationSpeed = 5;

    void Update()
    {
        transform.Rotate(this.transform.up, rotationSpeed * Time.deltaTime);        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }
        var manager = other.GetComponentInChildren<WeaponManager>();
        manager.m_Weapons.Add(gameObject);
        Destroy(GetComponent<Collider>());
        gameObject.SetActive(false);
        gameObject.transform.position = manager.m_Weapons[1].transform.position;
        gameObject.transform.rotation = manager.m_Weapons[1].transform.rotation;
        gameObject.transform.parent = manager.m_Weapons[1].transform.parent;
        gameObject.GetComponent<Shoot>().enabled = true;
        gameObject.GetComponent<Recoil>().enabled = true;
        Destroy(this);
    }
}

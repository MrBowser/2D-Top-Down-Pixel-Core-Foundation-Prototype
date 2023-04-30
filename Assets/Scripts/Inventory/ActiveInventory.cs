using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0;


    private PlayerControls playerControls;


    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        //note this is a bit of a weird quirck where we have to add a scale factor that corresponds with the number keys
        //otherwise if you press 2 it returns 1 (since it is just triggering an is true
        playerControls.Inventory.Keyboard.performed += ctx => ToogleActiveSlot((int)ctx.ReadValue<float>());

        //this just means we start at the sword or slot 1
        
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void ToogleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue-1);
        
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        //not this is setting false all of the child objects in the transform... reading it as for each transform in/under this transform
        //which is 5 inventory slots and then the children under those you are setting inactive
        foreach(Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        //once everything is inactive then take what index you pressed and turn it back on and make the associated value your active weap
        //with the changeactiveweapon function

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if(ActiveWeapon.Instance.CurrentActiveWeapon!= null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }
        //the below gets the correct inventory slot object and returns correct weapon info from the inventory slot script
        //they then instantiate it
        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        //the below is just the above 4 lines in one line of code
        //GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab;

        

        

        GameObject newWeapon = Instantiate(weaponToSpawn , ActiveWeapon.Instance.transform.position, Quaternion.identity);
        
        //note the below line helps with the new weapon following the mouse and not tweaking out
        ActiveWeapon.Instance.transform.rotation= Quaternion.Euler(0,0,0);
        
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }


}

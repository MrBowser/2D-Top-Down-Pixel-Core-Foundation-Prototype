using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    //singleton is a programming pattern that is rigged up below to be a specific type of monobehavior that if it 
    //exists you will destroy other instance on load as well as the first version is not destroyed on load
    //objectname.instance is how you reference the singleton object, then you can get all the other methods in that associated script/object

    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        //the first if statement is if another instance is attempted then destroy it 
        //note in the singletcon classes derived from this we need to call base.awake in that class to get this awake and the other awakes to trigger
        if(instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T)this;
        }


        //this prevents the initial object from being destroyed
        //explanation for the if statement is in Camera Controller video
        //essentially since the manager object is present it needs to be a singleton since root objects can't be singletons and needs the if
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

        

    }
    
}

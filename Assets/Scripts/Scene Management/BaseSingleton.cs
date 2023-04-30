using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSingleton : Singleton<BaseSingleton>
{ 
    //note this just exists so that the managers game object that houses the other singleton doesn't get destroyed and wonk out the hieararchy

}

interface IWeapon
{

    //this allows likeminded objects to trigger this function and not need to jigger up each specific weapon class 
    //ex. all weapons have a specific attack function, in activeweapon we call Attack() as iweapon and the correct attack method will trigger
    //this is called an interface

    public void Attack();

    public WeaponInfo GetWeaponInfo();

}
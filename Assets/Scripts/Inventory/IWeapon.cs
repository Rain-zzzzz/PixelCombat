internal interface IWeapon//只写签名，没有方法体,部重写接口里的成员
{
    public void Attach();

    public WeaponInfo GetWeaponInfo();
}
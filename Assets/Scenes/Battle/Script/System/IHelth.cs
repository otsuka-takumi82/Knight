interface IHelth
{
    float CurrentHp { get; }
    float MaxHp { get; }
    // Update is called once per frame
    void ModifyHelth(float amount);
}

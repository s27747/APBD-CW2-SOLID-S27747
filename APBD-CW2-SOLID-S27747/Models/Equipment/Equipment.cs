namespace APBD_CW2_SOLID_S27747.Models.Equipment;

public abstract class Equipment
{
    public int Id { get; private set; }
    public string Name { get; }
    public EquipmentStatus Status { get; private set; }

    protected Equipment(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Equipment name cannot be empty.");

        Name = name;
        Status = EquipmentStatus.Available;
    }

    internal void SetId(int id)
    {
        Id = id;
    }

    public void MarkAsBorrowed()
    {
        Status = EquipmentStatus.Borrowed;
    }

    public void MarkAsAvailable()
    {
        Status = EquipmentStatus.Available;
    }

    public void MarkAsUnavailable()
    {
        Status = EquipmentStatus.Unavailable;
    }

    public abstract string GetSpecificInfo();
}
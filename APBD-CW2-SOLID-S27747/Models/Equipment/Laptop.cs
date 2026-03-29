namespace APBD_CW2_SOLID_S27747.Models.Equipment;

public class Laptop : Equipment
{
    public string Processor { get; }
    public int RamGb { get; }

    public Laptop(string name, string processor, int ramGb) : base(name)
    {
        if (string.IsNullOrWhiteSpace(processor))
            throw new ArgumentException("Processor cannot be empty.");

        if (ramGb <= 0)
            throw new ArgumentException("RAM must be greater than zero.");

        Processor = processor;
        RamGb = ramGb;
    }

    public override string GetSpecificInfo()
    {
        return $"Processor: {Processor}, RAM: {RamGb} GB";
    }
}
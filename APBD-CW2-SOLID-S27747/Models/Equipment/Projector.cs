namespace APBD_CW2_SOLID_S27747.Models.Equipment;

public class Projector : Equipment
{
    public string Resolution { get; }
    public int BrightnessLumens { get; }

    public Projector(string name, string resolution, int brightnessLumens) : base(name)
    {
        if (string.IsNullOrWhiteSpace(resolution))
            throw new ArgumentException("Resolution cannot be empty.");

        if (brightnessLumens <= 0)
            throw new ArgumentException("Brightness must be greater than zero.");

        Resolution = resolution;
        BrightnessLumens = brightnessLumens;
    }

    public override string GetSpecificInfo()
    {
        return $"Resolution: {Resolution}, Brightness: {BrightnessLumens} lm";
    }
}
using System;

namespace APBD_CW2_SOLID_S27747.Models.Equipments;

public class Camera : Equipment
{
    public string SensorType { get; }
    public int OpticalZoom { get; }

    public Camera(string name, string sensorType, int opticalZoom) : base(name)
    {
        if (string.IsNullOrWhiteSpace(sensorType))
            throw new ArgumentException("Sensor type cannot be empty.");

        if (opticalZoom <= 0)
            throw new ArgumentException("Optical zoom must be greater than zero.");

        SensorType = sensorType;
        OpticalZoom = opticalZoom;
    }

    public override string GetSpecificInfo()
    {
        return $"Sensor: {SensorType}, Optical zoom: {OpticalZoom}x";
    }
}
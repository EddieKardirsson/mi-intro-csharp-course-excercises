namespace ProteusSmartHome.Models;

public abstract class SmartDevice
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string? Description { get; set; }

    protected SmartDevice(string name, string manufacturer, string model, string? description)
    {
        Name = name;
        Manufacturer = manufacturer;
        Model = model;
        Description = description;
    }

    public abstract void TurnOn();
    public abstract void TurnOff();
}

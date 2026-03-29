using APBD_CW2_SOLID_S27747.Models.Equipments;

namespace APBD_CW2_SOLID_S27747.Repositories.Interfaces;

public interface IEquipmentRepository
{
    Equipment Add(Equipment equipment);
    Equipment? GetById(int id);
    IReadOnlyList<Equipment> GetAll();
    IReadOnlyList<Equipment> GetAvailable();
}
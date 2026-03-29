using APBD_CW2_SOLID_S27747.Models.Equipments;
using APBD_CW2_SOLID_S27747.Repositories.Interfaces;
using APBD_CW2_SOLID_S27747.Services.Results;

namespace APBD_CW2_SOLID_S27747.Services;

public class EquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public Equipment AddEquipment(Equipment equipment)
    {
        return _equipmentRepository.Add(equipment);
    }

    public IReadOnlyList<Equipment> GetAllEquipment()
    {
        return _equipmentRepository.GetAll();
    }

    public IReadOnlyList<Equipment> GetAvailableEquipment()
    {
        return _equipmentRepository.GetAvailable();
    }

    public OperationResult MarkAsUnavailable(int equipmentId)
    {
        var equipment = _equipmentRepository.GetById(equipmentId);

        if (equipment is null)
            return OperationResult.Fail("Equipment not found.");

        if (equipment.Status == EquipmentStatus.Borrowed)
            return OperationResult.Fail("Borrowed equipment cannot be marked as unavailable.");

        if (equipment.Status == EquipmentStatus.Unavailable)
            return OperationResult.Fail("Equipment is already unavailable.");

        equipment.MarkAsUnavailable();
        return OperationResult.Ok($"Equipment {equipment.Name} was marked as unavailable.");
    }
}
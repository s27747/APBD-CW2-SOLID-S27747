using System.Collections.Generic;
using System.Linq;
using APBD_CW2_SOLID_S27747.Models.Equipments;
using APBD_CW2_SOLID_S27747.Repositories.IdGenerators;
using APBD_CW2_SOLID_S27747.Repositories.Interfaces;

namespace APBD_CW2_SOLID_S27747.Repositories;

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _equipment = [];
    private readonly EquipmentIdGenerator _idGenerator;

    public InMemoryEquipmentRepository(EquipmentIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public Equipment Add(Equipment equipment)
    {
        equipment.SetId(_idGenerator.NextId());
        _equipment.Add(equipment);
        return equipment;
    }

    public Equipment? GetById(int id)
    {
        return _equipment.FirstOrDefault(e => e.Id == id);
    }

    public IReadOnlyList<Equipment> GetAll()
    {
        return _equipment.AsReadOnly();
    }

    public IReadOnlyList<Equipment> GetAvailable()
    {
        return _equipment
            .Where(e => e.Status == EquipmentStatus.Available)
            .ToList()
            .AsReadOnly();
    }
}
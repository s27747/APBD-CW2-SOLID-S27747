namespace APBD_CW2_SOLID_S27747.Repositories.IdGenerators;

public class UserIdGenerator
{
    private int _currentId;

    public int NextId()
    {
        _currentId++;
        return _currentId;
    }
}
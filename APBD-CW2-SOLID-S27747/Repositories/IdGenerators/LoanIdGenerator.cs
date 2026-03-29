namespace APBD_CW2_SOLID_S27747.Repositories.IdGenerators;

public class LoanIdGenerator
{
    private int _currentId;

    public int NextId()
    {
        _currentId++;
        return _currentId;
    }
}
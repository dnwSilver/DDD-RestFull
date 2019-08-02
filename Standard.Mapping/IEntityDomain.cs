namespace Standard.Mapping
{
    /// <summary>
    /// Сущность в доменной области.
    /// </summary>
    public interface IEntityDomain
    {
        int Id { get; }

        double Bal { get; }

        string Name { get; }
    }
}

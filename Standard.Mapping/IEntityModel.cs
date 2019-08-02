namespace Standard.Mapping
{
    /// <summary>
    /// Отображение доменной сущности в базе данных.
    /// </summary>
    public interface IEntityModel
    {
        int Id { get; }

        double Balance { get; }

        string CustomerName { get; }
    }
}

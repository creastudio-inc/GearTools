namespace AOEntityFramework.Validation
{
    public interface IValidation<TEntity> where TEntity : BaseEntity
    {
        bool IsValid(TEntity entity);
    }

}

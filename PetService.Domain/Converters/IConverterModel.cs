namespace PetService.Domain.Converters
{
    public interface IConvertModel<TSource, TTarget>
    {
        TTarget Convert();
    }
}
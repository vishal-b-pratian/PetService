using System.Collections.Generic;
using System.Linq;
using PetService.Domain.Converters;

namespace PetService.Domain.Extensions
{
    public static class ConvertedExtensions
    {
        public static IEnumerable<TTarget> ConvertAll<TSource, TTarget>(
           this IEnumerable<IConvertModel<TSource, TTarget>> values)
           => values.Select(value => value.Convert());
    }
}

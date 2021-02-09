using System;

namespace Metozis.System.Entities
{
    public interface IReproducible
    {
        Type GetGeneratorType();
    }
}
using Metozis.System.Entities;
using Metozis.System.Generators.StellarGeneration;
using Metozis.System.Interaction;
using NotImplementedException = System.NotImplementedException;

namespace Metozis.System.Stellar
{
    public class GalaxyMapObject : Entity, ISelectable
    {
        public readonly StellarSystemGenerationOptions GenerationOptions;
        
        protected override void InitializeModules()
        {
            
        }

        public void Open()
        {
            
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        public void DeSelect()
        {
            throw new NotImplementedException();
        }
    }
}
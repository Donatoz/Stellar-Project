using Metozis.System.Entities;
using Metozis.System.Extensions.Copy.CopyContexts;
using Metozis.System.Extensions.Copy.PasteContexts;
using UnityEditor;

namespace Metozis.System.Extensions.Copy
{
    public class EditorCopyExtensions
    {
        [MenuItem("CONTEXT/Object/Copy/Meta/Copy meta-data", false, 1)]
        public static void CopyMetaFields(MenuCommand command)
        {
            var obj = command.context;
            CopyBuffer.ReadFields(obj);
        }
        
        [MenuItem("CONTEXT/Object/Copy/Meta/Paste meta-data", false, 2)]
        public static void PasteMetaFields(MenuCommand command)
        {
            var obj = command.context;
            CopyBuffer.PasteFields(obj);
        }
        
        [MenuItem("CONTEXT/Planet/Planet Options/Copy/Material/Copy material data", false, 3)]
        public static void CopyPlanetMaterialData(MenuCommand command)
        {
            var obj = (Planet) command.context;
            CopyBuffer.MakeCopy(obj, new PlanetMaterialCopyContext());
        }
        
        [MenuItem("CONTEXT/Planet/Planet Options/Copy/Material/Paste material data", false, 3)]
        public static void PastePlanetMaterialData(MenuCommand command)
        {
            var obj = (Planet) command.context;
            CopyBuffer.PasteCopy(obj, new PlanetMaterialPasteContext());
        }
        
        [MenuItem("CONTEXT/Planet/Planet Options/Copy/Meta/Paste material to meta", false, 3)]
        public static void PlanetMaterialToMeta(MenuCommand command)
        {
            var obj = (Planet) command.context;
            CopyBuffer.PasteCopy(obj, new PlanetMaterialToTemplatePasteContext());
        }
    }
}
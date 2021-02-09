
using Metozis.System.Entities;
using Metozis.System.Entities.Modules;
using Metozis.System.Management;
using UnityEngine;

namespace Metozis.System.Stellar
{
    public class GalaxyVisualModule : Module
    {
        private Galaxy galaxy => Target as Galaxy;
        private Transform effectsRoot;
        
        public GalaxyVisualModule(Entity target) : base(target)
        {
            effectsRoot = galaxy.transform.Find("Effects");
        }
        
        public void DrawEdges()
        {
            var edgesRoot = galaxy.transform.Find("Edges");
            var edgeTemplate = ManagersRoot.Get<SpawnManager>().LoadResource<GameObject>("Utils/GalaxyEdge");
            var offset = new Vector3(10, 10, 10);
            
            foreach (var edge in galaxy.Structure.Edges)
            {
                var edgeObj = ManagersRoot.Get<SpawnManager>()
                    .InstantiateObject(edgeTemplate, edgesRoot.transform.position, Quaternion.identity);
                var lineRenderer = edgeObj.GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, edge.Source.transform.position + offset);
                lineRenderer.SetPosition(1, edge.Target.transform.position + offset);
                edgeObj.transform.parent = edgesRoot;
            }
        }
    }
}
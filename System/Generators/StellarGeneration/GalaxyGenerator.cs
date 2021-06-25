using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Metozis.System.Entities;
using Metozis.System.Extensions;
using Metozis.System.Generators.Preprocessors;
using Metozis.System.IO;
using Metozis.System.Management;
using Metozis.System.Registry;
using Metozis.System.Stellar;
using QuikGraph;
using QuikGraph.Algorithms;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Metozis.System.Generators.StellarGeneration
{
    public class GalaxyGenerator : EntityGenerator
    {
        protected override string TemplateName => "Galaxy";

        public override Entity Generate(GenerationOptions options, IGeneratorPostprocessor postprocessor = null)
        {
            if (!Validate<GalaxyGenerationOptions>(options)) return null;
            var genOptions = options as GalaxyGenerationOptions;
            
            var galaxy = ManagersRoot.Get<SpawnManager>()
                .InstantiateObject(template, Vector3.zero, Quaternion.identity).GetComponent<Galaxy>();
            galaxy.Name = options.Name;
            
            // Stage 1 : Spawn and place all stellars randomly around galaxy center.
            
            // TODO: Do templates for stellars
            var dummy = Resources.Load<GameObject>("DummyPoint");
            var stellars = new GameObject[genOptions.StellarsAmount];

            var galaxyBody = galaxy.transform.Find("Body");

            for (int i = 0; i < genOptions.StellarsAmount; i++)
            {
                stellars[i] = ManagersRoot.Get<SpawnManager>()
                    .InstantiateObject(dummy, Vector3.zero, Quaternion.identity);
                stellars[i].transform.parent = galaxyBody;
                stellars[i].name = $"System {i + 1}";
            }
            

            RandomPlacement(genOptions, stellars);
            
            // Stage 2 : Get minimum spanning tree using Prim's algorithm
            
            var graph = ConstructGraph(stellars);
            var MST = graph.MinimumSpanningTreePrim(edge =>
                    Vector3.Distance(edge.Source.transform.position, edge.Target.transform.position));
            
            // Stage 3 : Triangulate
            
            var edges = AdditionalConnections(MST.ToList(), stellars, genOptions);
            
            // Stage 4 : Initialize galaxy

            var casted = edges
                .Select(e =>
                    new Edge<GalaxyMapObject>(e.Source.GetComponent<GalaxyMapObject>(),
                        e.Target.GetComponent<GalaxyMapObject>()));
            galaxy.Structure = casted.ToUndirectedGraph<GalaxyMapObject, Edge<GalaxyMapObject>>();
            galaxy.Structure.AddVertexRange(stellars.Select(s => s.GetComponent<GalaxyMapObject>()));
            galaxy.Visual.DrawEdges();
            
            // Stage 5 : Generate systems

            var reader = new RegistryReader();
            reader.Read(Global.PathVariables.MetozisRoot + "/Registry/Names/StarNames.rgm");

            foreach (var stellar in stellars)
            {
                stellar.GetComponent<GalaxyMapObject>().GenerationOptions = GenerateSystem(reader.Get());
            }
            
            return galaxy;
        }

        private StellarSystemGenerationOptions GenerateSystem(string name)
        {
            var options = new StellarSystemGenerationOptions();
            options.Name = name;
            return null;
        }

        private List<Edge<GameObject>> AdditionalConnections(List<Edge<GameObject>> edges, GameObject[] objects, GalaxyGenerationOptions genOptions)
        {
            //TODO: Implement Delaunay triangulation
            return edges;
        }

        private void RandomPlacement(GalaxyGenerationOptions genOptions, IEnumerable<GameObject> objects)
        {
            foreach (var obj in objects)
            {
                var point = UnityUtilities.RandomPositionInDonut(genOptions.RadiusMinMax);
                obj.transform.position = new Vector3(point.x, Random.Range(-genOptions.Height, genOptions.Height), point.y);
                //TODO: Remove after debug
                obj.transform.localScale = Vector3.one.UniformlyRandomizedBy(new Vector2(-0.5f, 0.5f));
            }
        }

        private UndirectedGraph<GameObject, Edge<GameObject>> ConstructGraph(GameObject[] objects)
        {
            var edges = new HashSet<Edge<GameObject>>();            
            for (int i = 0; i < objects.Length; i++)
            {
                for (int j = 0; j < objects.Length; j++)
                {
                    if (objects[i] == objects[j]) continue;
                    edges.Add(new Edge<GameObject>(objects[i], objects[j]));
                }
            }

            return edges.ToUndirectedGraph<GameObject, Edge<GameObject>>();
        }
    }
}
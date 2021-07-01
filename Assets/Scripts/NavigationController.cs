using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using System.Linq;
using Mapbox.Unity.MeshGeneration.Factories;

public class NavigationController : MonoBehaviour
{
    CustomSpawnOnMap SpawnScript;

    public DropDown dropSC;


    Dictionary<string, string> wayPoints = new Dictionary<string, string>();

    void Start()
    {

        SpawnScript = FindObjectOfType<CustomSpawnOnMap>();

        wayPoints.Add("Ágora"                       , "39.99462748472183,-0.06899925332420609");
        wayPoints.Add("ESTCE"                       , "39.99292500863973,-0.06714578790633553");
        wayPoints.Add("FCHS"                        , "39.995069680811625,-0.06998945459106296");
        wayPoints.Add("FCJE"                        , "39.99427946078642,-0.0661726370694918");
        wayPoints.Add("FCE"                         , "39.99436656101847,-0.06612569518093699");
        wayPoints.Add("Biblioteca"                  , "39.99408375855287,-0.06936995897610927");
        wayPoints.Add("Instalaciones Deportivas"    , "39.995125473921874,-0.07170665776320667");



        //print("Cosa de diccionario: " + wayPoints.ElementAt(0).);
        print("Cosa de diccionario: " + wayPoints.Count);

        dropSC.UpdateDropdown(wayPoints);
        SpawnScript.UpdatePOIs(wayPoints);

        //SetWaypoint(wayPoints["ESTCE"]);

        string a = "39.994942815440155 , -0.06287440572707582";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWaypoint(string location) 
    {
       // 39.994942815440155, -0.06287440572707582;
        SpawnScript.AddNewLocation(location);
    }

    
    public string GetWaypointLocation(string name)
    {
        //wayPoints.TryGetValue(name, out loc);
        print("El waypoint es: "+ name);
        string value = wayPoints[name];
        print("Las coords son: "+ value);

        return value;
    }
}


namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
	using System.Linq;

	public class CustomSpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		[Geocode]
		string[] _locationStrings;
		Vector2d[] _locations;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		[SerializeField]
		List<string> POIs;	

		[SerializeField]
		Dictionary<string, string> Ey;

		[SerializeField]
		List<GameObject> _spawnedObjects;

		public GameObject directions;

		DropDown DDScript;
		NavigationController navSC;

		void Start()
		{
			DDScript = FindObjectOfType<DropDown>();
			navSC = FindObjectOfType<NavigationController>();

			Debug.Log("loc "+ _locationStrings.Length);
			Debug.Log("POIS:"+ POIs.Count);
            _locations = new Vector2d[_locationStrings.Length];
            //_locations = new Vector2d[POIs.Count];
            _spawnedObjects = new List<GameObject>();
            /*for (int i = 0; i < _locationStrings.Length; i++)
            {
                var locationString = _locationStrings[i];
                _locations[i] = Conversions.StringToLatLon(locationString);
                var instance = Instantiate(_markerPrefab);
                instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                _spawnedObjects.Add(instance);
            }*/
        }

		private void Update()
		{
			if (POIs.Count > 0)
			{
				//Debug.Log("Updateo");

				int count = _spawnedObjects.Count;
				for (int i = 0; i < count; i++)
				{
					var spawnedObject = _spawnedObjects[i];
					var location = _locations[i];
					spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
					spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				}
			}
		}

		public void AddNewLocation(string locationStrings /*string latitude, string longitude*/) 
		{
			//InitWaypoints(locationStrings);
			/*			string[] NewLocationsStrings=new string[2];
			_locationStrings = NewLocationsStrings;*/
			POIs.Add(locationStrings);
			UpdateWaypoints();
		}
		public void UpdateWaypoints() 
		{
			_locations = new Vector2d[POIs.Count];
			_spawnedObjects = new List<GameObject>();
			for (int i = 0; i < POIs.Count; i++)
			{
				var locationString = POIs[i];
				_locations[i] = Conversions.StringToLatLon(locationString);
				var instance =  Instantiate(_markerPrefab);
				instance.transform.SetParent(directions.transform);
				instance.name = "Waypoint "+ i;
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
			}
			Debug.Log("POIS:" + POIs.Count);

		}
		public void UpdatePOIs(Dictionary<string, string> waypoints)
		{
			for (int i = 0; i < waypoints.Count; i++)
			{
				POIs.Add(waypoints.ElementAt(i).Value);
			}
			UpdateWaypoints();
		}
		public void ClearWaypoints() 
		{

		}
	}
}

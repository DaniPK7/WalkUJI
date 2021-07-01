namespace Mapbox.Unity.MeshGeneration.Factories
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Linq;
    using System.IO;

    public class DropDown : MonoBehaviour
    {
        public NavigationController navSC;
        //public CustomDirections directionsSC;
        public int dropSelected;

        [SerializeField]
        TMP_Dropdown dropdown;
        

        private void Start()
        {

            dropdown = transform.GetComponent<TMP_Dropdown>();
            dropdown.options.Clear(); print("borrp");
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = "None" });

        }
        private void Update() 
        {
            if (Input.GetKeyDown(KeyCode.Space)) { print("Opciones: " + dropdown.options.Count); }
        }
        public void HandleInputData(int val)
        {
            dropSelected = val;
            print("Cambio a: " + val);
            print(dropdown.options[val].text);
            /*
            if (val == 0) 
            {
                print("None");
            } 
            if (val == 1) 
            {
                print("ESTCE");
            }
            if (val == 2) 
            {
                print("Biblio");
            }*/
        }

        public void UpdateDropdown(Dictionary<string, string> waypoints)
        {
            print("Cosa de diccionario pero en DD: " + waypoints.Count);

            for (int i = 0; i < waypoints.Count; i++)
            {
                print("Cosa de updatear DD");
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = waypoints.ElementAt(i).Key });
                
            }

            dropdown.RefreshShownValue();

        }
        

        public string GetDropdownSelection() 
        {
            string a = dropdown.options[dropSelected].text;
            print("aaaaa: "+ a);

            return navSC.GetWaypointLocation(a);
        }
        public string GetDropdownSelectionName() 
        {
            string a = dropdown.options[dropSelected].text;

            return a;
        }

    }
}

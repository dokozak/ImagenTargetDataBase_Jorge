using UnityEngine;

[System.Serializable]
public class Name
{ 
    //Pokemon names
    public string[] pokemones;

        //get new pokemones names with json
        public static Name CreateFromJSON(string jsonString)
        {
            //get new pokemon with json
            return JsonUtility.FromJson<Name>(jsonString);
        }




}

using System;
using UnityEngine;

[System.Serializable]
public class Pokemon
{  
   //Pokemon name
   public string name;
   //Pokemon type
   public string [] type;


    //Constructor of pokemon
    //Name - pokemon name
    public Pokemon(string name)
    {
        //Vinculate the name
        this.name = name;
    }

    //Equals override
    public override bool Equals(object obj)
    {
        return obj is Pokemon pokemon &&
               base.Equals(obj) &&
               name == pokemon.name;
    }

    //Hash code override
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), name);
    }

    //Pokemon ToString
    public override string ToString()
    {
        //Tipes have the pokemon
        string types = "";
        //Get types
        for (int i = 0; i < type.Length; i++)
        {
            //Indicate other type
            if (i != 0)
            {
                types += "and ";
            }
            //Get type
            types += type[i].ToString();

            
           
        }

        return name + " pokemon " + types + " type";
    }

    //get new pokemon with json
    public static Pokemon CreateFromJSON(string jsonString)
    {
        //get new pokemon with json
        return JsonUtility.FromJson<Pokemon>(jsonString);
    }
    
    
}

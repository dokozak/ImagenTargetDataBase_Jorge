
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AdministratorGUI : MonoBehaviour
{
    //TextInformation the player has
    public TextMeshProUGUI textTime;
    //TextInformation the player has
    public TextMeshProUGUI textPokemon;
    //Time max the player has
    private const float RESETTIME = 16;
    //Time that the player has
    private float time = RESETTIME;
    //Limit time you have
    private const int LIMITTIME = 0;
    //Point that the player has
    public static int point = 0;
    //Pokemones who have in this game
    private Name pokemones;
    //Pokemon who is selected
    public static string pokemonNow = "";
    //Button who start the next pokemon
    public Button button;
    //Mode of button
    public static bool isEnable;
    //Button imagen
    private Image image;
    //conversion of time
    private int realTime;
    //Google drive link
    string googleLink = "https://drive.google.com/uc?export=download&id=1fo8GwE-kJMWT-pbTEhJKHrkmb0LOeiVN";
    //0 Wait for the internet / 1 we getting the file / -1 wifi problem
    private int estatus = 0;
    private void Start()
    {
        //Get google drive file
        StartCoroutine(DownloadTextFile());
        //Get imagen button
        image = button.GetComponent<Image>();
    }
    private void Update()
    {
        //Wait
        if (estatus == 0)
            return;
        //Aplication end
        if(estatus == -1)
            UnityEngine.Application.Quit();

        //Change information for the player
        textTime.SetText("Time: " + realTime.ToString() + "S\nPoint: " + point.ToString() + "");
        textPokemon.SetText("Pokemon Name: \n" + pokemonNow);
        //Change the button state 
        button.enabled = isEnable;

        //End this update
        if (ImagesScript.isRecognize)
        {
            image.color = Color.green;
            return;
        }
        //Real time
        realTime = ((time -= Time.deltaTime) < 0) ? 0 : (int)time;

        //Comprobe if the time has end
        if (realTime <= LIMITTIME)
        {
            if (!isEnable) { point--; }
            
            time = 0;
            isEnable = true;
            ImagesScript.isRecognize = true;
            image.color = Color.green;
        }
       

    }

    //Change the variable pokemonNow to other random pokemon
    public void newRandomPokemon()
    {
        //Dont repeat the pokemon
        string pokemon = pokemonNow;
        while (pokemon.Equals(pokemonNow))
        //Get a new pokemon random
        pokemonNow = pokemones.pokemones[Random.Range(0, pokemones.pokemones.Length)];
    }

    //Continue the game
    public void changeImagen()
    {
        //Change the pokemon name and reset the time
        newRandomPokemon();
        //Delete prefav
        ImagesScript.deletePrefav();
        //Reset the time
        time = RESETTIME;
        //Delete the refenrence of pokemon
        ImagesScript.pokemon = null;
        //Change the button state 
        isEnable = false;
        ImagesScript.isRecognize = false;
        image.color = Color.red;
    }


    IEnumerator DownloadTextFile()
    {
        //Get the link
        UnityWebRequest request = UnityWebRequest.Get(googleLink);
        
        //Wait
        yield return request.SendWebRequest();
        
        //Comprobe if the file is accesible
        if (request.result != UnityWebRequest.Result.Success)
        {
            //Wifi error
            estatus = -1;
        }
        else
        {
            //Get file content
            string fileContent = request.downloadHandler.text;
            //get all pokemon with json
            pokemones = Name.CreateFromJSON(fileContent);
            //We get the file
            estatus = 1;
            changeImagen();
        }
    }

}

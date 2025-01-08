using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ImagesScript : MonoBehaviour
{
    //Comprobe if the player has used a card
    public static bool isRecognize;
    //New prefav
    private static GameObject prefav;
    //The other pokemon
    public static Pokemon pokemon2;
    //The static pokemon
    public static Pokemon pokemon;
    //Comprobe if the random pokemon is equals than this pokemon
    public void isThisPokemon()
    {
        //Indicate if any pokemon has been recognize
        if (isRecognize)
            return;
        try
        {
            //If you pokemon is the same who is selected, you get a point if not you lose point
            if (pokemon.name.Equals(AdministratorGUI.pokemonNow))
            {
                //Win point
                AdministratorGUI.point++;
                StartCoroutine(FetchGameObjectFromServer(prefabBundleUrl, prefabsBundleName, a, b, gameObject));
                

            }
            else
                //Lose point
                AdministratorGUI.point--;
               
            
            pokemon2 = pokemon;

        }
        catch (Exception e)
        {
            //If you pokemon is the same who is selected, you get a point if not you lose point
            if (pokemon2.name.Equals(AdministratorGUI.pokemonNow))
            {
                //Win point
                AdministratorGUI.point++;
                StartCoroutine(FetchGameObjectFromServer(prefabBundleUrl, prefabsBundleName, a, b, gameObject));


            }


            else
                //Lose point
                AdministratorGUI.point--;

        }
        


        //Change the button state
        AdministratorGUI.isEnable = true;
        //Indicate if any pokemon has been recognize
        isRecognize = true;

    }

    public static void deletePrefav()
    {
       Destroy(prefav);
    }


    private string prefabBundleUrl = "https://drive.google.com/uc?export=download&id=1Cz89lpSYtzhm09oBeLSwNmPDoZ47aFpu";
    private string prefabsBundleName = "Assets/StreamingAssets/prefapbundle.manifest";
    private uint a;
    private Hash128 b;
    AssetBundle assetBundle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator FetchGameObjectFromServer(string url, string manifestFileName, uint crcR, Hash128 hashR, GameObject gameObject)
    {

        //Get from generated manifest file of assetbundle.
        uint crcNumber = crcR;
        //Get from generated manifest file of assetbundle.
        Hash128 hashCode = hashR;
        UnityWebRequest webrequest =
           UnityWebRequestAssetBundle.GetAssetBundle(url, new CachedAssetBundle(manifestFileName, hashCode), crcNumber);


        webrequest.SendWebRequest();

        while (!webrequest.isDone)
        {
            Debug.Log(webrequest.downloadProgress);

        }

        
        assetBundle = DownloadHandlerAssetBundle.GetContent(webrequest);
        yield return assetBundle;
        if (assetBundle == null)
            yield break;

         string gameObjectName = Path.GetFileNameWithoutExtension("assets/resources/prefap/" + pokemon.name + ".prefab").ToString();
         //See the pokemon who you have been successful
         prefav = Instantiate(assetBundle.LoadAsset(gameObjectName) as GameObject, gameObject.transform.position, Quaternion.identity);
         prefav.transform.SetParent(gameObject.transform, true);
         assetBundle.Unload(false);


        yield return null;
    }
}

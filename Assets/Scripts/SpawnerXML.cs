using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
[RequireComponent (typeof(Spawner ))]

public class SpawnerXML : MonoBehaviour {
    public class SpawnerData
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    [XmlRoot]
    
    public class XMLContainer
    {
        [XmlArray]
        public SpawnerData[] data;

    }
    public string fileName;
    private Spawner spawner;
    private string fullPath;

    //Create XMLContainer
    private XMLContainer xmlContainer;
    void SaveToPath(string path) //Saves XMLContainer instance to a file as XML format
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XMLContainer));
        using (FileStream stream = new FileStream (path, FileMode.Create))   //using will close the file after
        {
            serializer.Serialize(stream, xmlContainer);
        }
    }
    XMLContainer Load (string path) //Loads XMLContainer from path
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XMLContainer));
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as XMLContainer;
        }
    }

    public void Save()
    {
        //SET objects to spawner.objects
        List<GameObject>  objects = spawner.objects;

        //SET xmlContainer to new XMLContainer
        xmlContainer = new XMLContainer ();

       
        //SET xmlContainer.data to new SpawnerData[objects.count]
        xmlContainer.data = new SpawnerData[objects.Count];
        //FOR i = 0 to objects.Count
        for (int i = 0; i < objects.Count; i++)
        {


            //Set data to new SpawnerData
            SpawnerData data = new SpawnerData();
            //SET item to objects[i]
            GameObject item = objects[i];
            //SET data's position to item's position
            data.position = item.transform.position;
            //SET data's rotation to item's rotation
            data.rotation = item.transform.rotation;
            //SET xmlContainer.data[i] to data 
            xmlContainer.data[i] = data;
        }
        //CALL SavetoPath(fullPath)
        SaveToPath(fullPath);
    }

    void Apply()
    {
        //SET data to xmlContainer.data
        SpawnerData[] data = xmlContainer.data;
        //FOR i = 0 to data.Length
        for (int i = 0; i < data.Length; i++)
        {
            SpawnerData d = data[i]; //SET d to data[i] 



            //CALL spawner.Spawn() and pass d.position,d.rotation 
            spawner.Spawn(d.position, d.rotation);
        }
    }
	// Use this for initialization
	void Start () {
        //Set spawner to Spawner Component;
        spawner = GetComponent<Spawner>();
        
        //SET fullPath to Application.dataPath + "/" + fileName + ".xml"
        fullPath = Application.dataPath + "/" + fileName + ".xml";

        //IF file exists to fullPath
        if (File.Exists(fullPath))
        {


            //SET xmlContainer to Load(fullPath)
            xmlContainer = Load(fullPath);

            //CALL Apply()  
            Apply();
        }
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

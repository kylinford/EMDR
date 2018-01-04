using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EMDR
{
    [Serializable]
    class DataContent
    {
        public List<string> believes = new List<string>();
    }

    public class Data
    {
        string dataPath = Application.persistentDataPath + "/EMDRData.dat";
        DataContent dataContent = new DataContent();

        public List<string> Believes { get { return dataContent.believes; } }

        public Data()
        {
            EMDRSettings settings = new EMDRSettings();

            if (!File.Exists(dataPath))
            {
                foreach (EMDRSettings_Believe believe in settings.Believes.Content)
                {
                    dataContent.believes.Add(believe.Text);
                }
                Save();
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(dataPath, FileMode.Open);
                DataContent dataFromFile = (DataContent)bf.Deserialize(file);
                file.Close();
                dataContent.believes = dataFromFile.believes;
            }
        }

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file;
            file = File.Open(dataPath, FileMode.Create);

            bf.Serialize(file, dataContent);
            file.Close();
        }
    }
}
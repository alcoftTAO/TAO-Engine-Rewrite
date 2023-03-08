using System;
using System.Collections.Generic;
using System.IO;

namespace TAO.Engine
{
    public static class ModelImportation
    {
        public static TModelData GetModelData(string ModelName, bool FromMods = false)
        {
            //IMPORTANT: the model file extension must be .tmd (TAO Model Data)
            string[] modelData = File.ReadAllLines((FromMods ? "Mods/" : "Assets/") + "Models/" + ModelName + ".tmd");
            List<TVector3> Vertices = new List<TVector3>();
            List<TVector4> Colors = new List<TVector4>();
            List<TVector2> TextureCoords = new List<TVector2>();
            string TextureName = "";
            string RenderMode = "";
            TModelData data = new TModelData();

            for (int i = 0; i < modelData.Length; i++)
            {
                string l = modelData[i].ToLower().Replace(".", ",");

                if (l.StartsWith("vert "))
                {
                    //Apply vertices
                    string[] sv = l.Substring("vert ".Length).Split(' ');

                    Vertices.Add(new TVector3(float.Parse(sv[0]), float.Parse(sv[1]), float.Parse(sv[2])));
                }
                else if (l.StartsWith("texcoord "))
                {
                    //Apply texture coords
                    string[] sv = l.Substring("texcoord ".Length).Split(' ');

                    TextureCoords.Add(new TVector2(float.Parse(sv[0]), float.Parse(sv[1])));
                }
                else if (l.StartsWith("texname "))
                {
                    //Apply texture
                    TextureName = l.Substring("texname ".Length).Replace(",", ".");
                }
                else if (l.StartsWith("color "))
                {
                    //Apply colors (in float, from 0 to 1)
                    string[] sv = l.Substring("color ".Length).Split(' ');

                    Colors.Add(new TVector4(float.Parse(sv[0]), float.Parse(sv[1]), float.Parse(sv[2]), float.Parse(sv[3])));
                }
                else if (l.StartsWith("bcolor "))
                {
                    //Apply colors (in bytes, from 0 to 255)
                    string[] sv = l.Substring("bcolor ".Length).Split(' ');

                    Colors.Add(new TVector4(int.Parse(sv[0]), int.Parse(sv[1]), int.Parse(sv[2]), int.Parse(sv[3])));
                }
                else if (l.StartsWith("ctbcolor "))
                {
                    //Apply colors (convert from float, from 0 to 1, to byte, from 0 to 255)
                    string[] sv = l.Substring("ctbcolor ".Length).Split(' ');

                    Colors.Add(new TVector4(
                        (int)(float.Parse(sv[0]) * 255),
                        (int)(float.Parse(sv[1]) * 255),
                        (int)(float.Parse(sv[2]) * 255),
                        (int)(float.Parse(sv[3]) * 255)
                    ));
                }
                else if (l.StartsWith("renmod "))
                {
                    //Apply render mode
                    RenderMode = l.Substring("renmod ".Length);
                }
            }

            data.Vertices = Vertices.ToArray();
            data.Colors = Colors.ToArray();
            data.TextureCoords = TextureCoords.ToArray();
            data.TextureName = TextureName;
            data.RenderMode = RenderMode;

            return data;
        }

        public static string[] ObjToTMD(string[] ObjData, string WriteToFile = "")
        {
            //WARNING: This will only create the vertices and the texcoords.
            //You probably will need to set some values manually.
            List<string> tmdData = new List<string>();

            for (int i = 0; i < ObjData.Length; i++)
            {
                string l = ObjData[i].ToLower();

                if (l.StartsWith("v "))
                {
                    tmdData.Add("vert " + l.Substring("v ".Length).Replace("  ", " "));
                }
                else if (l.StartsWith("vt "))
                {
                    tmdData.Add("texcoord " + l.Substring("vt ".Length).Replace("  ", " "));
                }
            }

            if (WriteToFile.Trim() != "")
            {
                DiskData.CreateFile("Mods/Models/" + WriteToFile + ".tmd");
                File.WriteAllLines("Mods/Models/" + WriteToFile + ".tmd", tmdData.ToArray());
            }

            return tmdData.ToArray();
        }

        public static string[] ObjToTMD(string ObjFileName, bool FromMods = false, string WriteToFile = "")
        {
            return ObjToTMD(File.ReadAllLines((FromMods ? "Mods/" : "Assets/") + "Models/" + ObjFileName + ".obj"), WriteToFile);
        }
    }

    public class TModelData
    {
        public TVector3[] Vertices = new TVector3[0];
        public TVector4[] Colors = new TVector4[0];
        public TVector2[] TextureCoords = new TVector2[0];
        public string TextureName = "";
        public string RenderMode = "";

        public override string ToString()
        {
            string tr = "";

            tr += "Vertices: \n";

            foreach (TVector3 vector in Vertices)
            {
                tr += "    " + vector + "\n";
            }

            tr += "Colors: \n";

            foreach (TVector4 vector in Colors)
            {
                tr += "    " + vector + "\n";
            }

            tr += "Texture Coordenates: \n";

            foreach (TVector2 vector in TextureCoords)
            {
                tr += "    " + vector + "\n";
            }

            tr += "Texture: " + TextureName;

            return tr;
        }
    }
}
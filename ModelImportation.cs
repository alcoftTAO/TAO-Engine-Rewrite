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
            bool AllowTransparency = true;
            TVector3 Position = TVector3.Zero;
            TVector3 Scale = TVector3.One;
            TModelData data = new TModelData();

            for (int i = 0; i < modelData.Length; i++)
            {
                string l = modelData[i];
                string ll = l.ToLower();

                if (ll.StartsWith("vert "))
                {
                    //Apply vertices
                    Vertices.Add(TBasicMath.StringToVector3(ll.Substring("vert ".Length)));
                }
                else if (ll.StartsWith("texcoord "))
                {
                    //Apply texture coords
                    TextureCoords.Add(TBasicMath.StringToVector2(ll.Substring("texcoord ".Length)));
                }
                else if (ll.StartsWith("texname "))
                {
                    //Apply texture
                    TextureName = l.Substring("texname ".Length);
                }
                else if (ll.StartsWith("color "))
                {
                    //Apply colors (in float, from 0 to 1)
                    Colors.Add(TBasicMath.StringToVector4(ll.Substring("color ".Length)));
                }
                else if (ll.StartsWith("bcolor "))
                {
                    //Apply colors (in bytes, from 0 to 255)
                    Colors.Add(TBasicMath.StringToVector4(ll.Substring("bcolor ".Length)) / 255);
                }
                else if (ll.StartsWith("renmod "))
                {
                    //Apply render mode
                    RenderMode = ll.Substring("renmod ".Length);
                }
                else if (ll == "distran")
                {
                    //Set transparency to false
                    AllowTransparency = false;
                }
                else if (ll == "enatran")
                {
                    //Set transparency to true
                    AllowTransparency = true;
                }
                else if (ll == "togtran")
                {
                    //Toggle transparency
                    AllowTransparency = !AllowTransparency;
                }
                else if (ll.StartsWith("pos "))
                {
                    //Apply position
                    Position = TBasicMath.StringToVector3(ll.Substring("pos ".Length));
                }
                else if (ll.StartsWith("scl "))
                {
                    //Apply position
                    Scale = TBasicMath.StringToVector3(ll.Substring("scl ".Length));
                }
            }

            data.Vertices = Vertices.ToArray();
            data.Colors = Colors.ToArray();
            data.TextureCoords = TextureCoords.ToArray();
            data.TextureName = TextureName;
            data.RenderMode = RenderMode;
            data.AllowTransparency = AllowTransparency;
            data.Position = Position;
            data.Scale = Scale;

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
                DiskData.CreateFile("Assets/Models/" + WriteToFile + ".tmd");
                File.WriteAllLines("Assets/Models/" + WriteToFile + ".tmd", tmdData.ToArray());
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
        public bool AllowTransparency = true;
        public TVector3 Position = TVector3.Zero;
        public TVector3 Scale = TVector3.One;

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
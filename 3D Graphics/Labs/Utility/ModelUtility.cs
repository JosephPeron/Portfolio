using System;
using System.IO;
using System.Collections.Generic;

namespace Labs.Utility
{
    /// <summary>
    /// Model Utility reads a very simple, inefficient file format that uses 
    /// Triangles only. This is not good practice, but it does the job :)
    /// </summary>
    public class ModelUtility
    {
        public float[] Vertices { get; private set; }
        public int[] Indices { get; private set; }

        private ModelUtility() { }

        private static ModelUtility LoadFromBIN(string pModelFile)
        {
            ModelUtility model = new ModelUtility();
            BinaryReader reader = new BinaryReader(new FileStream(pModelFile, FileMode.Open));

            int numberOfVertices = reader.ReadInt32();
            int floatsPerVertex = 6;

            model.Vertices = new float[numberOfVertices * floatsPerVertex];

            byte[]  byteArray = new byte[model.Vertices.Length * sizeof(float)];
            byteArray = reader.ReadBytes(byteArray.Length);

            Buffer.BlockCopy(byteArray, 0, model.Vertices, 0, byteArray.Length);

            int numberOfTriangles = reader.ReadInt32();

            model.Indices = new int[numberOfTriangles * 3];
            
            byteArray = new byte[model.Indices.Length * sizeof(int)];
            byteArray = reader.ReadBytes(model.Indices.Length * sizeof(int));
            Buffer.BlockCopy(byteArray, 0, model.Indices, 0, byteArray.Length);

            reader.Close();
            return model;
        }
        
        private static ModelUtility LoadFromOBJ(string pModelFile)
        {
            ModelUtility model = new ModelUtility();
            StreamReader reader;
            reader = new StreamReader(pModelFile);

            string line = "";
            List<float> verts = new List<float>();
            List<float> normals = new List<float>();
            List<int> indicesList = new List<int>();
            List<float> textureList = new List<float>();

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string[] lineSplit = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (lineSplit.Length == 0)
                {
                    continue;
                }

                switch (lineSplit[0])
                {
                    case "v":
                        for (int i = 1; i < 4; i += 1)
                        {
                            float number;
                            if (float.TryParse(lineSplit[i], out number))
                            {
                                verts.Add(number);
                            }
                            else
                            {
                                throw new Exception("Error when reading vertices in model file " + pModelFile + " " + lineSplit[i] + " is not a valid number");
                            }
                        }
                        break;

                    case "vn":
                        for (int i = 1; i < 4; i += 1)
                        {
                            float number;
                            if (float.TryParse(lineSplit[i], out number))
                            {
                                normals.Add(number);
                            }
                            else
                            {
                                throw new Exception("Error when reading vertices in model file " + pModelFile + " " + lineSplit[i] + " is not a valid number");
                            }
                        }
                        break;

                    case "f":
                        for (int i = 1; i < 4; i += 1)
                        {
                            string[] itemSplit = lineSplit[i].Split('/');
                            int number;
                            if (int.TryParse(itemSplit[0], out number))
                            {
                                indicesList.Add(number - 1);
                            }
                        }
                        break;
                }
            }

            if (verts.Count != normals.Count)
            {
                throw new Exception("OBJ file not compatible with our format, vertex and normal missmatch");
            }

            float[] modelVerts = new float[verts.Count * 2];

            int place = 0;
            for (int i = 0; i < verts.Count; i += 3)
            {
                modelVerts[place] = verts[i];
                modelVerts[place + 1] = verts[i + 1];
                modelVerts[place + 2] = verts[i + 2];
                place += 6;
            }

            place = 3;
            for (int i = 0; i < normals.Count; i += 3)
            {
                modelVerts[place] = normals[i];
                modelVerts[place + 1] = normals[i + 1];
                modelVerts[place + 2] = normals[i + 2];
                place += 6;
            }

            model.Vertices = modelVerts;
            model.Indices = indicesList.ToArray();

            reader.Close();
            return model;
        }

        private static ModelUtility LoadFromSJG(string pModelFile)
        {
            ModelUtility model = new ModelUtility();
            StreamReader reader;
            reader = new StreamReader(pModelFile);
            string line = reader.ReadLine(); // vertex format
            int numberOfVertices = 0;
            int floatsPerVertex = 6;
            if (!int.TryParse(reader.ReadLine(), out numberOfVertices))
            {
                throw new Exception("Error when reading number of vertices in model file " + pModelFile);
            }

            model.Vertices = new float[numberOfVertices * floatsPerVertex];

            string[] values;
            for (int i = 0; i < model.Vertices.Length; )
            {
                line = reader.ReadLine();
                values = line.Split(',');
                foreach(string s in values)
                {
                    if (!float.TryParse(s, out model.Vertices[i]))
                    {
                        throw new Exception("Error when reading vertices in model file " + pModelFile + " " + s + " is not a valid number");
                    }
                    ++i;
                }
            }

            reader.ReadLine();
            int numberOfTriangles = 0;
            line = reader.ReadLine();
            if (!int.TryParse(line, out numberOfTriangles))
            {
                throw new Exception("Error when reading number of triangles in model file " + pModelFile);
            }

            model.Indices = new int[numberOfTriangles * 3];

            for(int i = 0; i < numberOfTriangles * 3;)
            {
                line = reader.ReadLine();
                values = line.Split(',');
                foreach(string s in values)
                {
                    if (!int.TryParse(s, out model.Indices[i]))
                    {
                        throw new Exception("Error when reading indices in model file " + pModelFile + " " + s + " is not a valid index");
                    }
                    ++i;
                }
            }

            reader.Close();
            return model;
        }

        public static ModelUtility LoadModel(string pModelFile)
        {
            string extension = pModelFile.Substring(pModelFile.IndexOf('.'));

            if (extension == ".sjg")
            {
                return LoadFromSJG(pModelFile);
            }
            else if (extension == ".bin")
            {
                return LoadFromBIN(pModelFile);
            }
            else if (extension == ".obj")
            {
                return LoadFromOBJ(pModelFile);
            }
            else
            {
                throw new Exception("Unknown file extension " + extension);
            }
        }

    }
}

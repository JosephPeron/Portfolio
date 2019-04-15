using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Labs.Utility;

namespace Labs.ACW
{
    class SceneGraphDrawModel : SceneGraphNode
    {
        ModelUtility mModel;
        PrimitiveType mDrawType;
        int mVAO_ID;

        public SceneGraphDrawModel(ModelUtility pModel, PrimitiveType pDrawType, int pVAO_ID)
        {
            mModel = pModel;
            mDrawType = pDrawType;
            mVAO_ID = pVAO_ID;
        }       

        public void DrawModel()
        {
            GL.BindVertexArray(mVAO_ID);
            GL.DrawElements(mDrawType, mModel.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Labs.ACW
{
    class SceneGraphDrawElements : SceneGraphNode
    {
        int mVAO_ID;
        PrimitiveType mDrawType;
        int mIndicesToDraw;

        public SceneGraphDrawElements(int pVAO_ID, int pIndicesToDraw, PrimitiveType pDrawType)
        {
            mDrawType = pDrawType;
            mVAO_ID = pVAO_ID;
            mIndicesToDraw = pIndicesToDraw;
        }

        public void DrawElements()
        {
            GL.BindVertexArray(mVAO_ID);
            GL.DrawElements(mDrawType, mIndicesToDraw, DrawElementsType.UnsignedInt, 0);
        }
    }
}

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
    class SceneGraphDraw : SceneGraphNode
    {
        PrimitiveType mDrawType;
        int mVAO_ID;
        int mVertLength;

        public SceneGraphDraw(PrimitiveType pDrawType, int pVAO_ID, int pVertLength)
        {
            mDrawType = pDrawType;
            mVAO_ID = pVAO_ID;
            mVertLength = pVertLength;
        }

        public void Draw()
        {
            GL.BindVertexArray(mVAO_ID);
            GL.DrawArrays(mDrawType, 0, mVertLength);
        }
    }
}

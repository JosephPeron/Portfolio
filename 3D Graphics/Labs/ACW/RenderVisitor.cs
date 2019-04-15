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
    class RenderVisitor
    {
        Stack<Matrix4> mMatrixStack;
        Stack<int> mShaderStack;
        Stack<int> mTextureStack;

        public RenderVisitor()
        {
            mMatrixStack = new Stack<Matrix4>();
            mShaderStack = new Stack<int>();
            mTextureStack = new Stack<int>();
        }

        public void Visit(SceneGraphNode node)
        {
            Type t = node.GetType();

            if (t.Equals(typeof(SceneGraphNode)))
            {
                VisitNode(node);
            }
            else if (t.Equals(typeof(SceneGraphGroup)))
            {
                VisitGroup((SceneGraphGroup)node);
            }
            else if (t.Equals(typeof(SceneGraphTransformation)))
            {
                VisitTransformation((SceneGraphTransformation)node);
            }
            else if (t.Equals(typeof(SceneGraphShader)))
            {
                VisitShader((SceneGraphShader)node);
            }
            else if (t.Equals(typeof(SceneGraphTexture)))
            {
                VisitTexture((SceneGraphTexture)node);
            }
            else if (t.Equals(typeof(SceneGraphDrawModel)))
            {
                VisitDrawModel((SceneGraphDrawModel)node);
            }
            else if (t.Equals(typeof(SceneGraphDraw)))
            {
                VisitDraw((SceneGraphDraw)node);
            }
            else if (t.Equals(typeof(SceneGraphDrawElements)))
            {
                VisitDrawElements((SceneGraphDrawElements)node);
            }
        }

        private void VisitNode(SceneGraphNode pNode)
        {

        }

        private void VisitGroup(SceneGraphGroup pNode)
        {
            for (int i = 0; i < pNode.GetChildCount(); i += 1)
            {
                pNode.GetChild(i).Accept(this);
            }
        }

        private void VisitTransformation(SceneGraphTransformation pNode)
        {
            //add to stack
            PushTransform(pNode.GetLTM());
            //call children using visitGroup
            VisitGroup(pNode);
            //remove top of stack
            PopTransform();
        }

        private void VisitShader(SceneGraphShader pNode)
        {
            int shaderID = pNode.GetShaderID();
            mShaderStack.Push(shaderID);
            GL.UseProgram(shaderID);
            VisitGroup(pNode);
            mShaderStack.Pop();
            if(mShaderStack.Count > 0)
            {
                GL.UseProgram(mShaderStack.Peek());
            }
        }

        private void VisitTexture(SceneGraphTexture pNode)
        {
            int textureID = pNode.GetTextureID();
            mTextureStack.Push(textureID);
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            VisitGroup(pNode);
            mTextureStack.Pop();
        }

        private void VisitDrawModel(SceneGraphDrawModel pNode)
        {
            int uModel = GL.GetUniformLocation(mShaderStack.Peek(), "uModel");
            Matrix4 matrix = PeekTransform();
            GL.UniformMatrix4(uModel, true, ref matrix);
            pNode.DrawModel();
        }

        private void VisitDraw(SceneGraphDraw pNode)
        {
            int uModel = GL.GetUniformLocation(mShaderStack.Peek(), "uModel");
            Matrix4 matrix = PeekTransform();
            GL.UniformMatrix4(uModel, true, ref matrix);
            pNode.Draw();
        }

        private void VisitDrawElements(SceneGraphDrawElements pNode)
        {
            int uModel = GL.GetUniformLocation(mShaderStack.Peek(), "uModel");
            Matrix4 matrix = PeekTransform();
            GL.UniformMatrix4(uModel, true, ref matrix);
            pNode.DrawElements();
        }

        private void PushTransform(Matrix4 pMatrix)
        {

            if (mMatrixStack.Count == 0)
            {
               mMatrixStack.Push(pMatrix);
            }
            else
            {
                Matrix4 topStack = PeekTransform();
                Matrix4 newMatrix = pMatrix * topStack;
                mMatrixStack.Push(newMatrix);
            }
        }

        private void PopTransform()
        {
            //removes top transform from stack
            mMatrixStack.Pop();
        }

        private Matrix4 PeekTransform()
        {
            return mMatrixStack.Peek();
        }
    }
}
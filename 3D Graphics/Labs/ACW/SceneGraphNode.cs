using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.ACW
{
    class SceneGraphNode
    {
        public void Accept(RenderVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

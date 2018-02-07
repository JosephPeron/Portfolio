class RenderVisitor {
    constructor(pContext) {
        this.mStack = [];
        this.setContext(pContext);
    }

    getStack() {
        return this.mStack;
    }
    setStack(pStack) {
        this.mStack = pStack;
    }

    getContext() {
        return this.mContext;
    }
    setContext(pContext) {
        this.mContext = pContext;
    }

    visit(pNode) {
        var type = pNode.getType();
        //console.log("type: " + type);
        switch (type) {
            case "group":
                this.visitGroup(pNode);
                break;
            case "polygon":
                this.visitDraw(pNode);
                break;
            case "transform":
                this.visitTransform(pNode);
                 break;
            case "state":
                this.visitState(pNode);
                break;
            case "circle":
                this.visitDraw(pNode);
                break;
        }
    }

    visitGroup(pNode) {
        for (var i = 0; i < pNode.getChildNum(); i += 1) {
            var node = pNode.getSpecificChild(i);
            node.accept(this);
        }
    }

    visitDraw(pNode) {
        //get the top from the stack, set transform with it
        var topStack = this.peekTransform();
        topStack.setTransform(this.getContext());

        //call the draw of the node
        pNode.draw(this.getContext());
    }

    visitTransform(pNode) {
        //add to stack
        this.pushTransform(pNode.getLTM());
        //call children using visitGroup
        this.visitGroup(pNode);
        //remove top of stack
        this.popTransform();
    }

    visitState(pNode) {
        //console.log("state hit");
        var topic = pNode.getTopic();
        var value = pNode.getValue();
        var previousState = this.mContext[topic];
        this.mContext[topic] = value;
        this.visitGroup(pNode);
        this.mContext[topic] = previousState;
    }

    pushTransform(pMatrix) {
        //if there are 0 things in stack// add to stack
        if (this.mStack.length === 0) {
            this.mStack.push(pMatrix);   
        }
        //if not call peektransform(to get top of stack) and multiply old matrix by the new matrix and push it to the stack
        else {
            var topStack = this.peekTransform();
            var newMatrix = topStack.multiply(pMatrix);
            this.mStack.push(newMatrix);
        }
    }

    popTransform() {
        //removes top transform from stack
        this.mStack.pop();
    }

    peekTransform() {
        //get the length of stack minus 1 and return that number element
        var num = this.mStack.length - 1;
        //console.log(num);
        return this.mStack[num];
    }

}
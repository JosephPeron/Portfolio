class Circle extends Physics {

    constructor(pPosition, pRadius, pScale) {
        var mass = Math.PI * Math.pow(pRadius, 2) / 100;
        super(pPosition, new Vector(0, 0), mass);
        this.setPosition(pPosition);
        this.setScale(pScale);
        this.setRadius(pRadius);

        this.constructSceneGraph();
    }

    getType() {
        return "circle";
    }

    getPosition() {
        return this.mPosition;
    }
    setPosition(pPosition) {
        this.mPosition = pPosition;
    }

    getRadius() {
        return this.mRadius;
    }
    setRadius(pRadius) {
        this.mRadius = pRadius;
    }

    getScale() {
        return this.mScale;
    }
    setScale(pScale) {
        this.mScale = pScale;
    }

    getScaleNode() {
        return this.mScaleNode;
    }
    setScaleNode(pScaleNode) {
        this.mScaleNode = pScaleNode;
    }

    getCentreNode() {
        return this.mCentreNode;
    }
    setCentreNode(pCentreNode) {
        this.mCentreNode = pCentreNode;
    }

    getAngleNode() {
        return this.mAngleNode;
    }
    setAngleNode(pAngleNode) {
        this.mAngleNode = pAngleNode;
    }

    getTopNode() {
        return this.mTopNode;
    }
    setTopNode(pNode) {
        this.mTopNode = pNode;
    }

    constructSceneGraph() {
        //create scale node
        var scale = new Matrix.createScale(this.getScale());
        var scaleNode = new SceneGraphTransform(scale);
        this.setScaleNode(scaleNode);

        //create angle node
        var angle = new Matrix.createRotation(0);
        var angleNode = new SceneGraphTransform(angle);
        this.setAngleNode(angleNode);

        //create centre node
        var centre = new Matrix.createTranslation(this.getPosition());
        var centreNode = new SceneGraphTransform(centre);
        this.setCentreNode(centreNode);

        //create ball node
        var ballNode = new SceneGraphCircle(this.getRadius());

        //create state node
        var stateNode = new SceneGraphState("fillStyle", "#FF4848");

        stateNode.addChild(ballNode);
        scaleNode.addChild(stateNode);
        angleNode.addChild(scaleNode);
        centreNode.addChild(angleNode);

        this.setTopNode(centreNode);
    }

    update(deltaTime) {
        super.update(deltaTime);

        var newMatrix = Matrix.createTranslation(this.getPosition());
        this.getCentreNode().setLTM(newMatrix);
    }
}
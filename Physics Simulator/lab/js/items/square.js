class Square extends Physics{

    constructor(pPosition, pScale, pWidth, pHeight) {
        var mass = pWidth * pHeight;
        super(pPosition, new Vector(0, 0), mass);
        this.setPosition(pPosition);
        this.setScale(pScale);
        this.setWidth(pWidth);
        this.setHeight(pHeight);
        //always last
        this.constructSceneGraph();
    }

    getType() {
        return "box";
    }

    getWidth() {
        return this.mWidth;
    }
    setWidth(pWidth) {
        this.mWidth = pWidth;
    }

    getHeight() {
        return this.mHeight;
    }
    setHeight(pHeight) {
        this.mHeight = pHeight;
    }

    getPosition() {
        return this.mPos;
    }
    setPosition(pPos) {
        this.mPos = pPos;
    }

    getCentreNode() {
        return this.mCentreNode;
    }
    setCentreNode(pCentreNode) {
        this.mCentreNode = pCentreNode;
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

    getAngle() {
        return this.mAngle;
    }
    setAngle(pAngle) {
        this.mAngle = pAngle;
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
        //scene graph
        var scale = new Matrix.createScale(this.getScale());
        var scaleNode = new SceneGraphTransform(scale);
        this.setScaleNode(scaleNode);

        var angle = new Matrix.createRotation(0);
        var angleNode = new SceneGraphTransform(angle);
        this.setAngleNode(angleNode);

        var centre = new Matrix.createTranslation(this.getPosition());
        var centreNode = new SceneGraphTransform(centre);
        this.setCentreNode(centreNode);

        var width = this.getWidth();
        var height = this.getHeight();
        var pointsNode = new SceneGraphPolygon([new Vector(-width / 2, -height / 2), new Vector(width / 2, -height / 2), new Vector(width / 2, height / 2), new Vector(-width / 2, height / 2)]);
        var stateNode = new SceneGraphState("fillStyle", "#67D162");

        stateNode.addChild(pointsNode);
        scaleNode.addChild(stateNode);
        angleNode.addChild(scaleNode);
        centreNode.addChild(angleNode);

        this.setTopNode(centreNode);
    }

    getEdges() {
        var width = this.getWidth();
        var height = this.getHeight();
        var topLeftEdge = new Line(-(width) / 2, -(height) / 2);
        var topRightEdge = new Line(width / 2, -(height) / 2);
        var bottomRightEdge = new Line(width / 2, height / 2);
        var bottomLeftEdge = new Line(-(width) / 2, height / 2);
        edges.push(topLeftPoint);
        edges.push(topRightEdge);
        edges.push(bottomRightEdge);
        edges.push(bottomLeftEdge);

        for (var i = 0; i < edges.length; i += 1) {
            return (circle1, edges[i], edges[i + 1])
        }
    }

    update(deltaTime) {
        super.update(deltaTime);

        var newMatrix = Matrix.createTranslation(this.getPosition());
        this.getCentreNode().setLTM(newMatrix);
    }
}
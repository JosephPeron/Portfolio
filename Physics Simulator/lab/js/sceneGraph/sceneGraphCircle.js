class SceneGraphCircle extends SceneGraphNode {

    constructor(pRadius) {
        super();
        this.setRadius(pRadius);
    }

    getType() {
        return "circle";
    }

    getRadius() {
        return this.mRadius;
    }

    setRadius(pRadius) {
        this.mRadius = pRadius;
    }

    draw(pContext) {
        pContext.beginPath();
        pContext.arc(0, 0, this.getRadius(), 0, 2 * Math.PI, false);
        pContext.closePath();
        pContext.fill();
        pContext.stroke();
    }
}
    
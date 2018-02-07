class SceneGraphPolygon extends SceneGraphNode {

    constructor(pPoints) {
        super();
        this.setPoints(pPoints);
    }

    getType() {
        return "polygon";
    }
    
    getPoints() {
        return this.mPoints;
    }

    setPoints(pPoints) {
        this.mPoints = pPoints;
    }

    getPoint(pNum) {
        if (pNum < this.mPoints.length) {
            return this.mPoints[pNum];
        }
        else {
            return new Vector(0, 0, 1);
        }
    }

    draw(pContext) {
         pContext.beginPath();

         pContext.moveTo(this.getPoint(0).getX(), this.getPoint(0).getY());

         for (var i = 1; i < this.mPoints.length; i += 1) {
             pContext.lineTo(this.getPoint(i).getX(), this.getPoint(i).getY());
         }

         pContext.closePath();
         pContext.fill();
         pContext.stroke();
     }
     
}
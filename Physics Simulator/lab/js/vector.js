class Vector {

    constructor(pX, pY, pZ) {
        this.setX(pX);
        this.setY(pY);
        this.setZ(pZ);
    }

    getX() {
        return this.mX;
    }

    setX(pX) {
        this.mX = pX;
    }

    getY() {
        return this.mY;
    }

    setY(pY) {
        this.mY = pY;
    }

    getZ() {
        return this.mZ;
    }

    setZ(pZ) {
        if (pZ === undefined) {
            pZ = 1;
        }
        this.mZ = pZ;
    }

    add(pVector) {
        var newX, newY;

        newX = this.getX() + pVector.getX();
        newY = this.getY() + pVector.getY();

        return new Vector(newX, newY, 1);
    }

    subtract(pVector) {
        var newX, newY;

        newX = this.getX() - pVector.getX();
        newY = this.getY() - pVector.getY();

        return new Vector(newX, newY, 1);
    }

    multiply(pScale) {
        var newX, newY;

        newX = this.getX() * pScale;
        newY = this.getY() * pScale;

        return new Vector(newX, newY, 1);
    }

    divide(pScale) {
        var newX, newY;

        newX = this.getX() / pScale;
        newY = this.getY() / pScale;

        return new Vector(newX, newY, 1);
    }

    magnitude() {

        var xSquared, ySquared, xySquared, vectorScalar;

        xSquared = this.getX() * this.getX();
        ySquared = this.getY() * this.getY();
        xySquared = xSquared + ySquared;
        vectorScalar = Math.sqrt(xySquared);

        return vectorScalar;

    }

    normalise() {
        var vector = new Vector(0,0);

        if (this.magnitude() > 0) {
            vector = this.divide(this.magnitude());
        }

        return vector;
    }

    limitTo(pLimit) {
        var vector;
        var mag = this.magnitude();

        if (mag > pLimit) {
            vector = this.divide(mag / pLimit);

            return vector;
        }
        else {
            return new Vector(this.getX(), this.getY(), 1);
        }
    }

    dotProduct(pVector) {

        var pVectorX = pVector.getX();
        var pVectorY = pVector.getY();
        var VectorX = this.getX();
        var VectorY = this.getY();
        var scalarValue = (pVectorX * VectorX) + (pVectorY * VectorY);

        return scalarValue;
    }

    dotProductZ(pVector) {

        var pVectorX = pVector.getX();
        var pVectorY = pVector.getY();
        var pVectorZ = pVector.getZ();
        var VectorX = this.getX();
        var VectorY = this.getY();
        var VectorZ = this.getZ();
        var scalarValue = (pVectorX * VectorX) + (pVectorY * VectorY) + (pVectorZ * VectorZ);

        return scalarValue;
    }

    interpolate(pVector, pScalar) {

        var abVectorX = pVector.getX() - this.getX();
        var abVectorY = pVector.getY() - this.getY();
        var cVectorX = abVectorX * pScalar;
        var cVectorY = abVectorY * pScalar;
        
        return new Vector (cVectorX + this.getX(), cVectorY + this.getY(), 1)
    }

    rotate(pScalar) {

        var newX, newY;
        var cos = Math.cos(pScalar);
        var sin = Math.sin(pScalar);

        newX = (this.getX() * cos) - (this.getY() * sin);
        newY = (this.getX() * sin) + (this.getY() * cos);

        return new Vector (newX, newY, 1)
    }

    angleBetween(pVector) {

        var vectorDot = (pVector.getX() * this.getX()) + (pVector.getY() * this.getY());
        var magDot = pVector.magnitude() * this.magnitude();
        var cosAngle = vectorDot / magDot;

        var angle = Math.acos(cosAngle);

        return angle
      
    }

}
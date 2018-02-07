class Matrix {

    constructor(m1, m2, m3, m4, m5, m6, m7, m8, m9) {
        this.mNumbers = [];
        this.mNumbers.push(m1);
        this.mNumbers.push(m2);
        this.mNumbers.push(m3);
        this.mNumbers.push(m4);
        this.mNumbers.push(m5);
        this.mNumbers.push(m6);
        this.mNumbers.push(m7);
        this.mNumbers.push(m8);
        this.mNumbers.push(m9);
    }

    getElement(pRow, pColumn) {
        var element = (3 * pRow) + pColumn;

        return this.mNumbers[element];
    }

    multiply(pMatrix) {
        var matrixElements = [];
        for (var row = 0; row < 3; row += 1) {
            var thisVector = new Vector(this.getElement(row, 0), this.getElement(row, 1), this.getElement(row, 2));

            for (var col = 0; col < 3; col += 1) {
                var vec2 = new Vector(pMatrix.getElement(0, col), pMatrix.getElement(1, col), pMatrix.getElement(2, col));

                matrixElements.push( thisVector.dotProductZ(vec2));
            }
        }
        return new Matrix(matrixElements[0], matrixElements[1], matrixElements[2], matrixElements[3], matrixElements[4] ,matrixElements[5], matrixElements[6], matrixElements[7], matrixElements[8]);
    }

    multiplyVector(pVector) {
        var vectorX = pVector.getX();
        var vectorY = pVector.getY();
        var vectorZ = pVector.getZ();
        var xElement = (this.mNumbers[0] * vectorX) + (this.mNumbers[1] * vectorY) + (this.mNumbers[2] * vectorZ);
        var yElement = (this.mNumbers[3] * vectorX) + (this.mNumbers[4] * vectorY) + (this.mNumbers[5] * vectorZ);
        var zElement = (this.mNumbers[6] * vectorX) + (this.mNumbers[7] * vectorY) + (this.mNumbers[8] * vectorZ);
        return new Vector(xElement, yElement, zElement);
    }

    transform(pContext) {
        pContext.transform(this.mNumbers[0], this.mNumbers[3], this.mNumbers[1], this.mNumbers[4], this.mNumbers[2], this.mNumbers[5]);
    }

    setTransform(pContext) {
        pContext.setTransform(this.mNumbers[0], this.mNumbers[3], this.mNumbers[1], this.mNumbers[4], this.mNumbers[2], this.mNumbers[5]);
    }

    alert() {
        alert(this.mNumbers[0] + this.mNumbers[1] + this.mNumbers[2] + this.mNumbers[3] + this.mNumbers[4] + this.mNumbers[5] + this.mNumbers[6] + this.mNumbers[7] + this.mNumbers[8]);
    }

}

Matrix.createIdentity = function () {
    return new Matrix(1, 0, 0, 0, 1, 0, 0, 0, 1);
}

Matrix.createTranslation = function (pVector) {
    return new Matrix(1, 0, pVector.getX(), 0, 1, pVector.getY(), 0, 0, 1);
}

Matrix.createScale = function (pVector) {
    return new Matrix(1 * pVector.getX(), 0, 0, 0, 1 * pVector.getY(), 0, 0, 0, 1 * pVector.getZ());
}

Matrix.createRotation = function (pScalar) {
    return new Matrix(Math.cos(pScalar), -Math.sin(pScalar), 0, Math.sin(pScalar), Math.cos(pScalar), 0, 0, 0, 1);
}
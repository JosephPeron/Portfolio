class SceneGraphState extends SceneGraphGroup {

    constructor(pTopic, pValue) {
        super();
        this.setTopic(pTopic);
        this.setValue(pValue);
    }

    getType() {
        return "state";
    }

    getTopic() {
        return this.mTopic; 
    }

    setTopic(pTopic) {
        this.mTopic = pTopic;
    }

    getValue() {
        return this.mValue;
    }

    setValue(pValue) {
        this.mValue = pValue;
    }


}
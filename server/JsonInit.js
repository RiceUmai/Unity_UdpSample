module.exports = class JsonInit{
    constructor () {
        this.jsonObj = {
            "type" : null,

            own : {
                "address" : null, 
                "port" : null,
                "uuid" : null,

                pos : {"x" : null, "y": null, "z": null},
                rot : {"x" : null, "y": null, "z": null}
            },

            ribal : {
                "address" : null, 
                "port" : null,
                "uuid" : null,

                pos : {"x" : null, "y": null, "z": null},
                rot : {"x" : null, "y": null, "z": null}
            }
        }
    }
    get() {
        return this.jsonObj;
    } 
}
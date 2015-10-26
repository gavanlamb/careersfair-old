// Setup an event listener to make an API call once auth is complete
function onLinkedInLoad() {
    IN.Event.on(IN, "auth", getProfileData);
}

// Handle the successful return from the API call
function onSuccess(data) {
    var linkedin = JSON.parse(document.getElementById("formLinked").value);
    if (linkedin != "" || linkedin != null) {
        for (var i in linkedin) {
            if (linkedin[i].hasOwnProperty("linkedInConnections")) {
                var linkedInConnections = linkedin[i].linkedInConnections[0];
                if (linkedInConnections.hasOwnProperty("field")) {
                    var fieldName = linkedInConnections.field;
                    if (linkedInConnections.hasOwnProperty("value")) {
                        var linkedinValues = linkedInConnections.value;
                        var fieldValue = "";
                        for (var j in linkedinValues) {
                            if (data.hasOwnProperty(linkedinValues[j].linkedInField)) {
                                var linkedInFieldName = linkedinValues[j].linkedInField;
                                if (typeof data[linkedInFieldName] == "object") {
                                    for (var k in data[linkedInFieldName]) {
                                        if (k > 0) {
                                            fieldValue += " ";
                                        }
                                        if (typeof data[linkedInFieldName][k] != "object") {
                                            fieldValue += data[linkedInFieldName][k];
                                            document.getElementById(fieldName).value = fieldValue;
                                        }
                                    }
                                }
                                else {
                                    if (j > 0) {
                                        fieldValue += " ";
                                    }
                                    fieldValue += data[linkedInFieldName];
                                    document.getElementById(fieldName).value = fieldValue;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

// Use the API call wrapper to request the member's basic profile data
function getProfileData() {
    IN.API.Raw("/people/~:(id,first-name,last-name,maiden-name,formatted-name,phonetic-first-name,phonetic-last-name,formatted-phonetic-name,headline,location,industry,current-share,num-connections,num-connections-capped,summary,positions,picture-url,picture-urls::(original),site-standard-profile-request,api-standard-profile-request,public-profile-url,email-address)?format=json").result(onSuccess);
}
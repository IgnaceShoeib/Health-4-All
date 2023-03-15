mergeInto(LibraryManager.library, {
    RequestWatch: function getWatch() {
        let id;
        let heading;
        let successCallback = (position) => {
            heading = position.coords.heading;
            console.log(heading);
            unityInstance.SendMessage("LocationService", "GetWatch", heading);
            navigator.geolocation.clearWatch(id);
        };

        let errorCallback = (error) => {
            console.log(error);
        };

        id = navigator.geolocation.watchPosition(successCallback, errorCallback);
    },
    RequestLocation: function getLocation() {
        let location;
        let successCallback = (position) => {
            location = position.coords.latitude + "," + position.coords.longitude;
            console.log(location);
            unityInstance.SendMessage("LocationService", "GetLocation", location);
        };
  
        let errorCallback = (error) => {
            console.log(error);
        };
  
        navigator.geolocation.getCurrentPosition(successCallback, errorCallback);
    },
});
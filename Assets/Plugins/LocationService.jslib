mergeInto(LibraryManager.library, {
    RequestLocation: function getLocation() {
        var location;
        const successCallback = (position) => {
            location = position.coords.latitude + "," + position.coords.longitude;
            console.log(location);
            unityInstance.SendMessage("LocationService", "GetLocation", location);
        };
  
        const errorCallback = (error) => {
            console.log(error);
        };
  
        navigator.geolocation.getCurrentPosition(successCallback, errorCallback);
    }
});

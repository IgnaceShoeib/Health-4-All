mergeInto(LibraryManager.library, {
    RequestHeading: function getHeading() {
        window.addEventListener("deviceorientationabsolute", manageCompass, true);
        function manageCompass(event) {
            if (event.webkitCompassHeading) {
                absoluteHeading = event.webkitCompassHeading + 180;
            } else {
                absoluteHeading = 180 - event.alpha;
            }
            if (absoluteHeading != null)
            {
                unityInstance.SendMessage("LocationService", "GetHeading", absoluteHeading);
                window.removeEventListener("deviceorientationabsolute", manageCompass, true);
            }
            
        }
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
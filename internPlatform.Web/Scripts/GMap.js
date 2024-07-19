async function initMap() {
    
    // Data import
    const long = document.getElementById("Latitude");
    const lat = document.getElementById("Longitude");
    let longNumber = (long.value !== '')?  Number(long.value.replace(",", ".")) : null;
    let latNumber = (lat.value !== '') ? Number(lat.value.replace(",", ".")) : null;

    //console.log("long", long.value==='' )
    //console.log("lat", lat.value)
    //console.log("longNumber", longNumber)
    //console.log("latNumber", latNumber)
    //init in case of new map
    if (isNaN(latNumber) || isNaN(longNumber) || latNumber === null || longNumber === null  ) {
        latNumber = 47.02;
        longNumber = 28.83;
    }
    
    const { Map } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");
    const myLatlng = { lat: latNumber, lng: longNumber };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 14,
        center: myLatlng,
        mapId: "2c3ffc6aeaa7f60",
    });


    const icon = document.createElement("div");
    icon.innerHTML = '<i class="bi bi-flower1 h5"></i>';
    const faPin = new PinElement({
        glyph: icon,
        glyphColor: "#ffffff",
        background: "#ff005d",
        borderColor: "#ff005d",
    });
    let faMarker = new AdvancedMarkerElement({
        map,
        position: myLatlng,
        content: faPin.element,
        title: "A marker using a FontAwesome icon for the glyph.",
    });


    
    
    map.addListener("click", (mapsMouseEvent) => {
        faMarker = new AdvancedMarkerElement({
            map,
            position: mapsMouseEvent.latLng,
            content: faPin.element,
            title: "A marker using a FontAwesome icon for the glyph.",
        });
        long.value = mapsMouseEvent.latLng.lng().toFixed(6) ;
        lat.value = mapsMouseEvent.latLng.lat().toFixed(6);
       
    });
}


document.addEventListener('DOMContentLoaded', initMap);


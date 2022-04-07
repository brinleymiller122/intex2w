// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function pageForward(pageNum) {
    document.getElementById("pageNumber").value = pageNum + 1
    document.getElementById("crash-filter-form").submit()
}

function pageBackwards(pageNum) {
    document.getElementById("pageNumber").value = pageNum - 1
    document.getElementById("crash-filter-form").submit()
}

function revealRow(id) {
    document.getElementById("hiddenRow" + id).hidden= false
}

function hideRow(id) {
    document.getElementById("hiddenRow" + id).hidden = true
}

function predictionUpdate() {
    var data = {
        "intersection_related": 0,
        "night_dark_condition": 0,
        "older_driver_involved": 0,
        "teenage_driver_involved": 0,
        "single_vehicle": 0,
        "roadway_departure": 0,
        "milepoint_01": 0,
        "route_15": 0,
        "city_OUTSIDE_CITY_LIMITS": 0,
        "county_name_SALT_LAKE": 0,
        "county_name_UTAH": 0
    }
    var city = document.getElementById("citySelector").value
    var county = document.getElementById("countySelector").value
    var milepoint = document.getElementById("milepointInput").value
    var intersection = document.getElementById("intersectionRelated").value
    var teenager = document.getElementById("teenagerRelated").value
    var oldPeople = document.getElementById("olderRelated").value
    var nighttime = document.getElementById("nightRelated").value
    var singleVehicle = document.getElementById("singleVehicle").value
    var roadway = document.getElementById("roadWayDeparture").value
    var routeI15 = document.getElementById("i15").value

    if (city == "OUTSIDE CITY LIMITS") {
        data.city_OUTSIDE_CITY_LIMITS = 1
    }
    if (county == "UTAH") {
        data.county_name_UTAH = 1
    }
    if (county == "SALT LAKE") {
        data.county_name_SALT_LAKE = 1
    }
    if (milepoint == 0.1) {
        data.milepoint_01 = 1
    }
    if (routeI15 == true) {
        data.route_15 = 1
    }
    if (intersection == true) {
        data.intersection_related = 1
    }
    if (teenager == true) {
        data.teenage_driver_involved = 1
    }
    if (oldPeople == true) {
        data.older_driver_involved = 1
    }
    if (nighttime == true) {
        data.night_dark_condition = 1
    }
    if (singleVehicle == true) {
        data.single_vehicle = 1
    }
    if (roadway == 1) {
        data.roadway_departure = 1
    }

    $.ajax({
        type: "POST",
        url: "../Home/Score",
        data: {
            data: data
        },
        dataType: "json",
        success: function (data) {
            document.getElementById("predictedHeader").hidden = false
            document.getElementById("predictedBalue").style.opacity = 0;
            document.getElementById("predictedBalue").style.opacity = 1;
            document.getElementById("predictedBalue").innerHTML = data
        }
    })
}
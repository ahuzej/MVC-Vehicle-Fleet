window.onload = function () {

    var childDivs = document.getElementById('mainDisplay').getElementsByClassName('element');
    for (var i = 0; i < childDivs.length; i++) {
        var div = childDivs[i];
        var btn = div.getElementsByTagName('input')[0];
        btn.addEventListener("click", function () {
            var display = this.id.slice(0, -3);
            var element = document.getElementById(display);
            toggle(display);
        })
    }

    //document.getElementById('displayVozaciBtn').addEventListener("click", function () {
    //    var element = document.getElementById('displayVozaci');
    //    element.style.display = "none";
    //});
    //document.getElementById('displayPutniNalogBtn').addEventListener("click", function () {
    //    var element = document.getElementById('displayPutniNalog');
    //    element.style.display = "none";
    //});
    //document.getElementById('displayVozilaBtn').addEventListener("click", function () {
    //    var element = document.getElementById('displayVozila');
    //    element.style.display = "none";
    //});
}

function toggle(display) {
    var childDivs = document.getElementById('displayOptions').getElementsByTagName('div');
    for (var i = 0; i < childDivs.length; i++) {
        if (childDivs[i].id == display) {
            childDivs[i].style.display = "block";
        } else {
            childDivs[i].style.display = "none";
        }
    }
}
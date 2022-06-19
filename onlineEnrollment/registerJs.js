function openTab(evt, tab) {
    var i, tablinks, tabcontent, first;
    try {
        first = document.getElementById("first")
        first.id = first.id.replace("first", "")
    } catch (error) {

    }
    tabcontent = document.getElementsByClassName("container");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(tab).style.display = "block";
    evt.currentTarget.className += " active";
}
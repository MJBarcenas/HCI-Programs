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

function check(event) {
    var id = event.currentTarget.id;
    var obj = document.getElementsByClassName(id);
    var file = event.currentTarget.value;
    var pictures = ['Ffile', 'Tfile'];
    if (pictures.includes(id)) {
        var allowed = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
        if (!allowed.exec(file)) {
            obj[0].style = 'background-color: red';
            obj[0].innerHTML = 'Invalid file, Try again!';

            file.value = "";
            return false;
        } else {
            obj[0].style = 'background-color: dodgerblue';
            obj[0].innerHTML = 'Great!';
        }
    } else {
        var allowed = /(\.pdf)$/i;
        if (!allowed.exec(file)) {
            obj[0].style = 'background-color: red';
            obj[0].innerHTML = 'Invalid file, Try again!';
            alert('wrong!');

            file.value = "";
            return false;
        } else {
            obj[0].style = 'background-color: dodgerblue';
            obj[0].innerHTML = 'Great!';
        }
    }
}
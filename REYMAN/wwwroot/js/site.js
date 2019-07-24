// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myFunction() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue, pos;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    th = table.getElementsByTagName("th");
    pos = 0;
    for (i = 0; i < th.length; i++) {
        if (th[i].value === "selected")
            pos = i;
    }
    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[pos];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
//function select(th)
//{
//    table = document.getElementById("myTable");
//    ths = table.getElementsByTagName("th");
//    for (var i = 0; i < ths.length; i++) {
//        ths[i].value = "";
//        ths[i].style.color="white";
//    }
//    th.style.color = "blue";
//    th.value = "select";
//    console.log(th);
//}
function set() {
    table = document.getElementById("myTable");
    ths = table.getElementsByTagName("th");
    for (var i = 0; i < ths.length; i++) {
        ths[i].addEventListener("click", function () {
            if (this.innerText === "")
                return;
            input = document.getElementById("myInput");
            input.placeholder = "Buscar por " + this.innerText;
            input.size = input.placeholder.length;
            table = document.getElementById("myTable");
            ths = table.getElementsByTagName("th");
            for (var i = 0; i < ths.length; i++) {
                ths[i].style.color = "white";
                ths[i].value = "";
            }
            this.style.color = "blue";
            this.value = "selected";
        });
    }
    ths[0].click();
}

function drop() {
    /* Loop through all dropdown buttons to toggle between hiding and showing its dropdown content - This allows the user to have multiple dropdowns without any conflict */
    var dropdown = document.getElementsByClassName("dropdown-btn");
    var i;

    for (i = 0; i < dropdown.length; i++) {
        dropdown[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var dropdownContent = this.nextElementSibling;
            if (dropdownContent.style.display === "block") {
                dropdownContent.style.display = "none";
            } else {
                dropdownContent.style.display = "block";
            }
        });
    }
}
drop();
set();

function int_validate(e, val) {
    if (e.keyCode === 8 || e.keyCode === 39 || e.keyCode === 37 || e.keyCode === 46 || !isNaN(e.key))
        return true;
    return false;
}

function parse(caller, def) {
    if (caller.value !== "")
        caller.value = parseInt(caller.value);
    else
        caller.value = def;
}

function double_validate(e, val) {
    if (int_validate(e, val)) return true;
    var dot = val.split(',').length + val.split('.').length;
    if (dot === 2 && (e.key === "." || e.key === ",")) {
        return true;
    }
    return false;
}

function precision(caller, digits) {
    var symbols = [',', '.'];
    for (var a in symbols) {
        var num = caller.value.split(symbols[a]);
        if (num.length === 2) {
            caller.value = parseInt(num[0]) + symbols[a] + num[1].substring(0, digits);
            return;
        }
    }
    parse(caller, "");
}


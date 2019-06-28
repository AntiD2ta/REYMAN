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
        if (th[i].value == "selected")
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
    input = document.getElementById("myInput");
    input.placeholder = "Buscar por " + ths[0].innerText;
    ths[0].style.color = "blue";
    ths[0].value = "selected";
    for (var i = 0; i < ths.length; i++) {
        ths[i].addEventListener("click", function (e) {
            if (this.innerText == "")
                return;
            input = document.getElementById("myInput");
            input.placeholder = "Buscar por " + this.innerText;
            th = this;
            table = document.getElementById("myTable");
            ths = table.getElementsByTagName("th");
            for (var i = 0; i < ths.length; i++) {
                ths[i].value = "";
                ths[i].style.color = "white";
            }
            th.style.color = "blue";
            th.value = "selected";
        });
    }
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
$(document).ready(function () {
    $("#FilterType").change(function () {
        $("tr[data-val]").prop('hidden', true);
        var filter = $("#FilterType").val();
        $("tr[data-val="+filter+"]").prop('hidden', false);
    });
});

function addPerson() {
    var f = document.getElementById("FName").value;
    var m = document.getElementById("MName").value;
    var l = document.getElementById("LName").value;
    var email = document.getElementById("Email").value;
    var type = document.getElementById("Type").value;

    var data = { 'f': f, 'm': m, 'l': l, 'email': email, 'type': type };

    $.ajax({
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        type: "POST",
        cache: false,
        url: "/Home/AddPerson",
        success: function (result) {
            location.reload();
        },
        error: function () {
            alert("Error adding a person");
        }
    });
}

function clearFields() {
    document.getElementById("FName").value = "";
    document.getElementById("MName").value = "";
    document.getElementById("LName").value = "";
    document.getElementById("Email").value = "";
}

function deleteRow(rank) {
    var data = { 'rank': rank };

    $.ajax({
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        type: "POST",
        cache: false,
        url: "/Home/DeletePerson",
        success: function (result) {
            location.reload();
        },
        error: function () {
            alert("Error deleting person");
        }
    });
}

function moveUp(rankId) {
    var row = $("tr[rank-val=" + rankId + "]");

    var row = this.parentNode.parentNode,
        sibling = row.previousElementSibling,
        anchor = row.nextElementSibling,
        parent = row.parentNode;

    parent.insertBefore(row, sibling);
}
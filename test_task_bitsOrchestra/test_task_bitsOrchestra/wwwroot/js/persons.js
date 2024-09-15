function filterData() {
    var filterValue = document.getElementById("marriedFilter").value;
    var rows = document.querySelectorAll(".person-row");

    rows.forEach(function (row) {
        var isMarried = row.getAttribute("data-married") === "True";
        if (filterValue === "" || (filterValue === "true" && isMarried) || (filterValue === "false" && !isMarried)) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
}

var sortDirections = [true, true, true, true, true];

function sortTable(columnIndex) {
    var table = document.getElementById("personsTable");
    var rows = Array.from(table.rows).slice(1);
    var arrowElements = [
        document.getElementById("nameSortArrow"),
        document.getElementById("dobSortArrow"),
        document.getElementById("marriedSortArrow"),
        document.getElementById("phoneSortArrow"),
        document.getElementById("salarySortArrow")
    ];

    var ascending = sortDirections[columnIndex];
    var isNumericColumn = columnIndex === 4;
    var isDateColumn = columnIndex === 1;
    var isBooleanColumn = columnIndex === 2;

    rows.sort(function (rowA, rowB) {
        var cellA, cellB;


        if (columnIndex === 0) {
            cellA = rowA.querySelector("input[id^='name']").value;
            cellB = rowB.querySelector("input[id^='name']").value;
        } else if (columnIndex === 1) {
            cellA = rowA.querySelector("input[id^='dob']").value;
            cellB = rowB.querySelector("input[id^='dob']").value;
        } else if (columnIndex === 2) {
            cellA = rowA.querySelector("select[id^='married']").value;
            cellB = rowB.querySelector("select[id^='married']").value;
        } else if (columnIndex === 3) {
            cellA = rowA.querySelector("input[id^='phone']").value;
            cellB = rowB.querySelector("input[id^='phone']").value;
        } else if (columnIndex === 4) {
            cellA = parseFloat(rowA.querySelector("input[id^='salary']").value);
            cellB = parseFloat(rowB.querySelector("input[id^='salary']").value);
        }

        if (isNumericColumn) {
            return ascending ? cellA - cellB : cellB - cellA;
        }

        if (ascending) {
            return cellA.localeCompare(cellB);
        } else {
            return cellB.localeCompare(cellA);
        }
    });

    rows.forEach(function (row) {
        table.deleteRow(1);
    });

    rows.forEach(function (row) {
        table.tBodies[0].appendChild(row);
    });

    sortDirections[columnIndex] = !sortDirections[columnIndex];
    arrowElements.forEach(function (arrow, index) {
        arrow.innerText = "⬍";
    });

    arrowElements[columnIndex].innerText = ascending ? "⬆" : "⬇";
}

function validatePersonData(personId) {
    var name = document.getElementById(`name-${personId}`).value;
    var phone = document.getElementById(`phone-${personId}`).value;
    var salary = document.getElementById(`salary-${personId}`).value;

    var namePattern = /^[a-zA-Z\s]+$/;
    if (!namePattern.test(name)) {
        alert("Name should consist of only letters and spaces.");
        return false;
    }

    var phonePattern = /^\d{12}$/;
    if (!phonePattern.test(phone)) {
        alert("Phone number must consist of exactly 12 digits without spaces.");
        return false;
    }

    var salaryPattern = /^\d+(\.\d+)?$/;
    if (!salaryPattern.test(salary)) {
        alert("Salary must be a valid number (integer or decimal).");
        return false;
    }

    return true;
}

function savePerson(personId) {
    if (!validatePersonData(personId)) {
        return;
    }

    var name = document.getElementById(`name-${personId}`).value;
    var dob = document.getElementById(`dob-${personId}`).value;
    var married = document.getElementById(`married-${personId}`).value;
    var phone = document.getElementById(`phone-${personId}`).value;
    var salary = document.getElementById(`salary-${personId}`).value;

    var personData = {
        Id: personId,
        Name: name,
        DateOfBirth: dob,
        Married: married === "true",
        Phone: phone,
        Salary: parseFloat(salary)
    };

    fetch(`/Home/UpdatePerson`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(personData)
    })
        .then(response => {
            if (response.ok) {
                alert("Record updated successfully");
            } else {
                alert("Failed to update the record");
            }
        })
        .catch(error => {
            console.error("Error updating record:", error);
        });
}

function deletePerson(personId) {
    if (confirm("Are you sure you want to delete this record?")) {
        fetch(`/Home/DeletePerson/${personId}`, {
            method: "DELETE"
        })
            .then(response => {
                if (response.ok) {
                    document.querySelector(`tr[data-id='${personId}']`).remove();
                    alert("Record deleted successfully");
                } else {
                    alert("Failed to delete the record");
                }
            })
            .catch(error => {
                console.error("Error deleting record:", error);
            });
    }
}
// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {

    if (document.title == "Warehouse - Items") {
        checkAutoOpenAddItemModal();
        getItemGroupFromDropdown();
        getUnitFromDropdown();
        populateEditModal();
    }
});

// Checks whether the add item modal should be automatically open upon loading the page
function checkAutoOpenAddItemModal() {

    // Add item modal
    let modalAddItemTrigger = document.getElementById("modal-add-item-trigger");

    document.getElementById("modal-add-item-close-sign").addEventListener("click", () => {
        modalAddItemTrigger.value = "no";
    });

    document.getElementById("modal-add-item-close-btn").addEventListener("click", () => {
        modalAddItemTrigger.value = "no";
    });

    if (modalAddItemTrigger.value == "yes") {
        modalAddItemTrigger.click();
    }

    // Edit item modal
    let modalEditItemTrigger = document.getElementById("modal-edit-item-trigger");

    document.getElementById("modal-edit-item-close-sign").addEventListener("click", () => {
        modalEditItemTrigger.value = "no";
    });

    document.getElementById("modal-edit-item-close-btn").addEventListener("click", () => {
        modalEditItemTrigger.value = "no";
    });

    if (modalEditItemTrigger.value == "yes") {
        modalEditItemTrigger.click();
    }
}

// Assigns the selected dropdown item group to its associated form
function getItemGroupFromDropdown() {

    const itemGroupDropdownItems = document.getElementById("add-item-group-dropdown").getElementsByClassName("dropdown-item");
    let itemGroupButton = document.getElementById("add-item-group-dropdown-btn");
    let itemGroupInput = document.getElementById("add-item-group-input");

    if (itemGroupButton == null || itemGroupInput == null) {
        return;
    }

    for (let i = 0; i < itemGroupDropdownItems.length; i++) {
        itemGroupDropdownItems[i].addEventListener("click", (event) => {
            itemGroupButton.innerHTML = event.target.innerHTML;
            itemGroupInput.value = event.target.innerHTML;
        });
    }
}

// Assigns the selected dropdown unit to its associated form
function getUnitFromDropdown() {

    const unitDropdownItems = document.getElementById("add-item-unit-dropdown").getElementsByClassName("dropdown-item");
    let unitButton = document.getElementById("add-item-unit-dropdown-btn");
    let unitInput = document.getElementById("add-item-unit-input");

    if (unitButton == null || unitInput == null) {
        return;
    }

    for (let i = 0; i < unitDropdownItems.length; i++) {
        unitDropdownItems[i].addEventListener("click", (event) => {
            unitButton.innerHTML = event.target.innerHTML;
            unitInput.value = event.target.innerHTML;
        });
    }
}

// Populates the edit modal with the currently selected item data.
function populateEditModal() {
    let itemsTable = document.getElementById("items");
    const editButtons = recordsTable.getElementsByTagName("button");

    for (let i = 0; i < editButtons.length; i++) {
        editButtons[i].addEventListener("click", (event) => {

            let editRecordRow = event.target.id;

            document.getElementById("edit-record-id").value = editButtons[i].value;
            document.getElementById("habit-dropdown-button").innerHTML = recordsTable.rows[editRecordRow].cells[0].innerHTML;
            document.getElementById("edit-record-amount").value = recordsTable.rows[editRecordRow].cells[1].innerHTML;
            document.getElementById("edit-record-unit").value = recordsTable.rows[editRecordRow].cells[2].innerHTML;

            let rawDate = new Date(recordsTable.rows[editRecordRow].cells[3].innerHTML);
            let convertedDate = convertToDatetimeLocal(rawDate);

            document.getElementById("edit-record-date").value = convertedDate;

        });
    }
}
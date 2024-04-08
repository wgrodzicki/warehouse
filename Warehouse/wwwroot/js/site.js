// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {

    if (document.title == "Warehouse - Items") {
        /*checkAutoOpenAddItemModal();*/
        getItemGroupFromAddItemDropdown();
        getUnitFromAddItemDropdown();

        getItemGroupFromEditItemDropdown();
        getUnitFromEditItemDropdown();
        populateEditModal();

        checkAutoOpenAddItemModal();
        checkAutoOpenEditItemModal()

        //getSortingMode();
    }
});

// Automatically opens the add item modal after trying to add item with an invalid form.
function checkAutoOpenAddItemModal() {
    let modalAddItemTrigger = document.getElementById("modal-add-item-trigger");
    let modalAddItemButton = document.getElementById("modal-add-item-btn");

    document.getElementById("modal-add-item-close-sign").addEventListener("click", () => {
        modalAddItemTrigger.value = "no";
    });

    document.getElementById("modal-add-item-close-btn").addEventListener("click", () => {
        modalAddItemTrigger.value = "no";
    });

    if (modalAddItemTrigger.value == "yes") {
        modalAddItemButton.click();
    }
}

// Automatically opens the appropriate edit item modal after trying to update item with an invalid form.
function checkAutoOpenEditItemModal() {
    const modalEditItemTriggers = document.getElementById("table-items").getElementsByClassName("modal-edit-trigger");
    let editButtons = document.getElementById("table-items").getElementsByTagName("button");
    let modalEditItemIdHolder = document.getElementById("modal-edit-item-id-holder");

    document.getElementById("modal-edit-item-close-sign").addEventListener("click", () => {
        for (let i = 0; i < modalEditItemTriggers.length; i++) {
            modalEditItemTriggers[i].value = "no";
        }
    });

    document.getElementById("modal-edit-item-close-btn").addEventListener("click", () => {
        for (let i = 0; i < modalEditItemTriggers.length; i++) {
            modalEditItemTriggers[i].value = "no";
        }
    });

    for (let i = 0; i < modalEditItemTriggers.length; i++) {
        if (modalEditItemTriggers[i].value == "yes") {
            for (let j = 0; j < editButtons.length; j++) {
                if (editButtons[j].value == modalEditItemIdHolder.value) {
                    editButtons[j].click();
                    return;
                }
            }
        }
    }
}

// Handles selection from the add item item group dropdown.
function getItemGroupFromAddItemDropdown() {
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

// Handles selection from the add item unit dropdown.
function getUnitFromAddItemDropdown() {
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

// Handles selection from the edit item item group dropdown.
function getItemGroupFromEditItemDropdown() {
    const itemGroupDropdownItems = document.getElementById("edit-item-group-dropdown").getElementsByClassName("dropdown-item");
    let itemGroupButton = document.getElementById("edit-item-group-dropdown-btn");
    let itemGroupInput = document.getElementById("edit-item-group-input");

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

// Handles selection from the edit item unit dropdown.
function getUnitFromEditItemDropdown() {
    const unitDropdownItems = document.getElementById("edit-item-unit-dropdown").getElementsByClassName("dropdown-item");
    let unitButton = document.getElementById("edit-item-unit-dropdown-btn");
    let unitInput = document.getElementById("edit-item-unit-input");

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
    let itemsTable = document.getElementById("table-items");
    const editButtons = itemsTable.getElementsByTagName("button");

    for (let i = 0; i < editButtons.length; i++) {
        editButtons[i].addEventListener("click", (event) => {

            let editItemRow = event.target.id;

            document.getElementById("edit-item-id").value = editButtons[i].value;
            document.getElementById("edit-item-name").value = itemsTable.rows[editItemRow].cells[1].innerHTML;
            document.getElementById("edit-item-group-input").value = itemsTable.rows[editItemRow].cells[2].innerHTML;
            document.getElementById("edit-item-group-dropdown-btn").innerHTML = itemsTable.rows[editItemRow].cells[2].innerHTML;
            document.getElementById("edit-item-unit-input").value = itemsTable.rows[editItemRow].cells[3].innerHTML;
            document.getElementById("edit-item-unit-dropdown-btn").innerHTML = itemsTable.rows[editItemRow].cells[3].innerHTML;
            document.getElementById("edit-item-quantity").value = itemsTable.rows[editItemRow].cells[4].innerHTML;
            document.getElementById("edit-item-price").value = itemsTable.rows[editItemRow].cells[5].innerHTML;
            document.getElementById("edit-item-status").value = itemsTable.rows[editItemRow].cells[6].innerHTML;
            document.getElementById("edit-item-storage").value = itemsTable.rows[editItemRow].cells[7].innerHTML;
            document.getElementById("edit-item-contact").value = itemsTable.rows[editItemRow].cells[8].innerHTML;
        });
    }
}

//function getSortingMode() {
//    const sortButtons = document.getElementById("table-items").getElementsByClassName("btn-sm");
//    const sortInputs = document.getElementsByClassName("sort-input");

//    for (let i = 0; i < sortButtons.length; i++) {
//        sortButtons[i].addEventListener("click", (event) => {
//            if (sortButtons[i].id.includes("desc")) {
//                sortInputs[i].value = "desc";
//            }

//            if (sortButtons[i].id.includes("asc")) {
//                sortInputs[i].value = "asc";
//            }
//        });
//    }
//}
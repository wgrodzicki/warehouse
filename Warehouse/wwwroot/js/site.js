// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {

    if (document.title == "Warehouse - Items") {
        checkAutoOpenAddItemModal();
        getItemGroupFromDropdown();
        getUnitFromDropdown();
    }
});

function checkAutoOpenAddItemModal() {
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
}

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
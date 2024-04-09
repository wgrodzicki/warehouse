// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let pricePerItem = 0;

document.addEventListener("DOMContentLoaded", () => {

    if (document.title == "Warehouse - Items") {
        if (document.getElementById("modal-add-item-btn") != null) {
            getItemGroupFromAddItemDropdown();
            getUnitFromAddItemDropdown();
            checkAutoOpenAddItemModal();
        }

        if (document.getElementsByClassName("modal-edit-trigger").length >= 0) {
            getItemGroupFromEditItemDropdown();
            getUnitFromEditItemDropdown();
            populateEditModal();
            checkAutoOpenEditItemModal();
        }

        if (document.getElementsByClassName("modal-order-trigger").length >= 0) {
            populateOrderItemModal();
            updatePriceInOrderModal();
            checkAutoOpenOrderItemModal();
            checkAutoOpenRequestConfirmModal();
        }
    }
});

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

// Automatically opens the appropriate order item modal after trying to place an order with an invalid form.
function checkAutoOpenOrderItemModal() {
    const modalOrderItemTriggers = document.getElementById("table-items").getElementsByClassName("modal-order-trigger");
    let orderButtons = document.getElementById("table-items").getElementsByTagName("button");
    let modalOrderItemIdHolder = document.getElementById("modal-order-item-id-holder");

    document.getElementById("modal-order-item-close-sign").addEventListener("click", () => {
        for (let i = 0; i < modalOrderItemTriggers.length; i++) {
            modalOrderItemTriggers[i].value = "no";
        }
    });

    document.getElementById("modal-order-item-close-btn").addEventListener("click", () => {
        for (let i = 0; i < modalOrderItemTriggers.length; i++) {
            modalOrderItemTriggers[i].value = "no";
        }
    });

    for (let i = 0; i < modalOrderItemTriggers.length; i++) {
        if (modalOrderItemTriggers[i].value == "yes") {
            for (let j = 0; j < orderButtons.length; j++) {
                if (orderButtons[j].value == modalOrderItemIdHolder.value) {
                    orderButtons[j].click();
                    return;
                }
            }
        }
    }
}

// Automatically opens the request confirmation modal after placing a valid order.
function checkAutoOpenRequestConfirmModal() {
    let modalRequestConfirmTrigger = document.getElementById("modal-request-confirm-trigger");

    document.getElementById("modal-request-confirm-close-sign").addEventListener("click", () => {
        modalRequestConfirmTrigger.value = "no";
    });

    document.getElementById("modal-request-confirm-close-btn").addEventListener("click", () => {
        modalRequestConfirmTrigger.value = "no";
    });

    if (modalRequestConfirmTrigger.value == "yes") {
        modalRequestConfirmTrigger.click();
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

// Populates the order modal with the currently selected item data.
function populateOrderItemModal() {
    let itemsTable = document.getElementById("table-items");
    const orderButtons = itemsTable.getElementsByTagName("button");

    for (let i = 0; i < orderButtons.length; i++) {
        orderButtons[i].addEventListener("click", (event) => {

            let orderItemRow = event.target.id;

            document.getElementById("order-item-id").value = orderButtons[i].value;
            document.getElementById("order-item-name").value = itemsTable.rows[orderItemRow].cells[2].innerHTML;
            document.getElementById("order-item-unit").value = itemsTable.rows[orderItemRow].cells[4].innerHTML;
            document.getElementById("order-item-quantity").value = itemsTable.rows[orderItemRow].cells[5].innerHTML;
            document.getElementById("order-item-quantity").max = itemsTable.rows[orderItemRow].cells[5].innerHTML;

            let totalPrice = itemsTable.rows[orderItemRow].cells[5].innerHTML * itemsTable.rows[orderItemRow].cells[6].innerHTML;
            document.getElementById("order-item-price").value = totalPrice.toFixed(2);
            pricePerItem = itemsTable.rows[orderItemRow].cells[6].innerHTML;
        });
    }
}

// Updates the total order price in the order modal depending on the quantity input and the original item price.
function updatePriceInOrderModal() {
    let quantityInput = document.getElementById("order-item-quantity");
    let priceInput = document.getElementById("order-item-price");

    quantityInput.addEventListener("change", () => {
        priceInput.value = (pricePerItem * quantityInput.value).toFixed(2);
    });
}
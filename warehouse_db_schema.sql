CREATE TABLE items (
    id INTEGER NOT NULL UNIQUE,
    name TEXT NOT NULL,
    item_group TEXT NOT NULL,
    unit TEXT NOT NULL,
    quantity INTEGER NOT NULL,
    price_no_vat REAL NOT NULL,
    status TEXT NOT NULL,
    storage_location TEXT,
    contact_person TEXT,
    PRIMARY KEY(id AUTOINCREMENT)
);

CREATE TABLE requests (
    id INTEGER NOT NULL UNIQUE,
    item_id INTEGER NOT NULL,
    employee_name TEXT NOT NULL,
    unit TEXT NOT NULL,
    quantity INTEGER NOT NULL,
    price_no_vat REAL NOT NULL,
    comment TEXT,
    status TEXT DEFAULT "New",
    PRIMARY KEY(id AUTOINCREMENT),
    FOREIGN KEY(item_id) REFERENCES items(id)
);
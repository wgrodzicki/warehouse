CREATE TABLE items (
    id INTEGER NOT NULL UNIQUE,
    name TEXT NOT NULL,
    item_group_id INTEGER NOT NULL,
    unit_id INTEGER NOT NULL,
    quantity INTEGER NOT NULL,
    price_no_vat REAL NOT NULL,
    status TEXT NOT NULL,
    storage_location TEXT,
    contact_person TEXT,
    PRIMARY KEY(id AUTOINCREMENT),
    FOREIGN KEY(item_group_id) REFERENCES item_groups(id) ON DELETE CASCADE,
    FOREIGN KEY(unit_id) REFERENCES units(id) ON DELETE CASCADE
);

CREATE TABLE requests (
    id INTEGER NOT NULL UNIQUE,
    item_id INTEGER NOT NULL,
    employee_name TEXT NOT NULL,
    quantity INTEGER NOT NULL,
    price_no_vat REAL NOT NULL,
    comment_employee TEXT,
    comment_coordinator TEXT,
    status_id INTEGER NOT NULL,
    PRIMARY KEY(id AUTOINCREMENT),
    FOREIGN KEY(item_id) REFERENCES items(id) ON DELETE CASCADE,
    FOREIGN KEY(status_id) REFERENCES request_statuses(id) ON DELETE CASCADE
);

CREATE TABLE item_groups (
	id INTEGER NOT NULL UNIQUE,
    name  TEXT NOT NULL,
    PRIMARY KEY(id AUTOINCREMENT)
);

CREATE TABLE units (
	id INTEGER NOT NULL UNIQUE,
    name  TEXT NOT NULL,
    PRIMARY KEY(id AUTOINCREMENT)
);

CREATE TABLE request_statuses (
	id INTEGER NOT NULL UNIQUE,
    name TEXT NOT NULL,
    PRIMARY KEY(id AUTOINCREMENT)
);
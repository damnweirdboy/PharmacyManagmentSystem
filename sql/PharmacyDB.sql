-- ============================================================
-- PharmacyDB.sql
-- Full SQL Server database creation script for
-- Pharmacy Management System
-- Visual Programming Midterm Project
-- ============================================================

-- ── Step 1: Drop & Create Database ────────────────────────────
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'PharmacyDB')
BEGIN
    ALTER DATABASE PharmacyDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PharmacyDB;
END
GO

CREATE DATABASE PharmacyDB;
GO

USE PharmacyDB;
GO

-- ── Step 2: Create Tables ──────────────────────────────────────

-- Table 1: Admins
CREATE TABLE Admins (
    AdminId  INT           IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50)  NOT NULL,
    Password NVARCHAR(50)  NOT NULL,
    FullName NVARCHAR(100)
);
GO

-- Table 2: Medicines
CREATE TABLE Medicines (
    MedicineId   INT            IDENTITY(1,1) PRIMARY KEY,
    MedicineName NVARCHAR(100)  NOT NULL,
    Category     NVARCHAR(50),
    Price        DECIMAL(10,2)  NOT NULL,
    Quantity     INT            NOT NULL,
    ExpiryDate   DATE
);
GO

-- Table 3: Suppliers
CREATE TABLE Suppliers (
    SupplierId   INT            IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(100)  NOT NULL,
    Phone        NVARCHAR(20),
    Email        NVARCHAR(100),
    Address      NVARCHAR(200)
);
GO

-- Table 4: Customers
CREATE TABLE Customers (
    CustomerId   INT            IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100)  NOT NULL,
    Phone        NVARCHAR(20),
    Email        NVARCHAR(100),
    Address      NVARCHAR(200)
);
GO

-- Table 5: Sales (with Foreign Keys)
CREATE TABLE Sales (
    SaleId       INT            IDENTITY(1,1) PRIMARY KEY,
    MedicineId   INT            NOT NULL,
    CustomerId   INT            NOT NULL,
    QuantitySold INT            NOT NULL,
    TotalPrice   DECIMAL(10,2)  NOT NULL,
    SaleDate     DATETIME       NOT NULL,

    CONSTRAINT FK_Sales_Medicines
        FOREIGN KEY (MedicineId) REFERENCES Medicines(MedicineId),

    CONSTRAINT FK_Sales_Customers
        FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
GO

-- ── Step 3: Insert Default Admin ──────────────────────────────
INSERT INTO Admins (Username, Password, FullName)
VALUES ('admin', '1234', 'System Administrator');
GO

-- ── Step 4: Insert Sample Medicines ───────────────────────────
INSERT INTO Medicines (MedicineName, Category, Price, Quantity, ExpiryDate) VALUES
('Paracetamol 500mg',    'Analgesic',      2.50,  200, '2026-12-31'),
('Amoxicillin 250mg',    'Antibiotic',     8.75,  150, '2025-06-30'),
('Ibuprofen 400mg',      'Anti-inflammatory', 3.20, 180, '2026-08-15'),
('Cetirizine 10mg',      'Antihistamine',  4.00,  100, '2026-10-01'),
('Metformin 500mg',      'Antidiabetic',   5.50,   80, '2025-11-20'),
('Omeprazole 20mg',      'Antacid',        6.00,   90, '2026-07-14'),
('Atorvastatin 10mg',    'Lipid Lowering', 12.00,  60, '2026-03-31'),
('Aspirin 100mg',        'Analgesic',      1.80,  250, '2027-01-01'),
('Azithromycin 500mg',   'Antibiotic',    15.00,   40, '2025-09-10'),
('Vitamin C 1000mg',     'Supplement',     3.50,  300, '2027-06-30'),
('Salbutamol Inhaler',   'Bronchodilator',18.00,   25, '2025-04-01'),
('Losartan 50mg',        'Antihypertensive',9.00,  70, '2026-05-20'),
('Diclofenac 50mg',      'Anti-inflammatory',4.50, 5,  '2024-01-01'),
('Pantoprazole 40mg',    'Antacid',        7.25,   8,  '2025-12-15'),
('Iron Supplement',      'Supplement',     4.00,   3,  '2026-09-30');
GO

-- ── Step 5: Insert Sample Suppliers ───────────────────────────
INSERT INTO Suppliers (SupplierName, Phone, Email, Address) VALUES
('MediCare Pharma Ltd',    '+1-555-0101', 'info@medicare.com',    '123 Health Ave, New York, NY'),
('Global Drug Suppliers',  '+1-555-0202', 'sales@globaldrug.com', '456 Pharma Blvd, Chicago, IL'),
('HealthPlus Distributors','+1-555-0303', 'orders@healthplus.com','789 Wellness Rd, Los Angeles, CA'),
('Sunrise Pharmaceuticals','+1-555-0404', 'contact@sunrise.com',  '321 Sunrise St, Houston, TX'),
('ProMed Solutions',       '+1-555-0505', 'info@promed.com',      '654 ProMed Lane, Phoenix, AZ');
GO

-- ── Step 6: Insert Sample Customers ───────────────────────────
INSERT INTO Customers (CustomerName, Phone, Email, Address) VALUES
('Alice Johnson',   '+1-555-1001', 'alice.johnson@email.com',   '10 Maple St, Boston, MA'),
('Bob Smith',       '+1-555-1002', 'bob.smith@email.com',       '22 Oak Ave, Denver, CO'),
('Carol Williams',  '+1-555-1003', 'carol.w@email.com',         '33 Pine Rd, Miami, FL'),
('David Brown',     '+1-555-1004', 'david.b@email.com',         '44 Cedar Blvd, Seattle, WA'),
('Eva Martinez',    '+1-555-1005', 'eva.m@email.com',           '55 Birch Lane, Atlanta, GA'),
('Frank Wilson',    '+1-555-1006', 'frank.w@email.com',         '66 Elm St, Dallas, TX'),
('Grace Lee',       '+1-555-1007', 'grace.l@email.com',         '77 Walnut Ave, Portland, OR'),
('Henry Taylor',    '+1-555-1008', 'henry.t@email.com',         '88 Spruce Ct, Nashville, TN');
GO

-- ── Step 7: Insert Sample Sales ───────────────────────────────
INSERT INTO Sales (MedicineId, CustomerId, QuantitySold, TotalPrice, SaleDate) VALUES
(1,  1,  5,  12.50, '2024-11-01 10:00:00'),
(2,  2,  2,  17.50, '2024-11-02 11:30:00'),
(3,  3,  3,   9.60, '2024-11-03 14:00:00'),
(4,  4,  1,   4.00, '2024-11-04 09:15:00'),
(5,  5,  4,  22.00, '2024-11-05 16:45:00'),
(6,  6,  2,  12.00, '2024-11-06 12:00:00'),
(8,  7, 10,  18.00, '2024-11-07 13:30:00'),
(10, 8,  6,  21.00, '2024-11-08 15:20:00'),
(1,  2,  3,   7.50, '2024-11-09 10:00:00'),
(12, 3,  2,  18.00, '2024-11-10 11:00:00');
GO

-- ── Step 8: Verification SELECT Statements ────────────────────
SELECT 'Admins Table'   AS [Table], COUNT(*) AS [Row Count] FROM Admins
UNION ALL
SELECT 'Medicines',  COUNT(*) FROM Medicines
UNION ALL
SELECT 'Suppliers',  COUNT(*) FROM Suppliers
UNION ALL
SELECT 'Customers',  COUNT(*) FROM Customers
UNION ALL
SELECT 'Sales',      COUNT(*) FROM Sales;
GO

-- Full data check
SELECT * FROM Admins;
GO

SELECT * FROM Medicines;
GO

SELECT * FROM Suppliers;
GO

SELECT * FROM Customers;
GO

-- Sales with joined names
SELECT
    s.SaleId,
    m.MedicineName,
    c.CustomerName,
    s.QuantitySold,
    s.TotalPrice,
    s.SaleDate
FROM Sales s
INNER JOIN Medicines m ON s.MedicineId = m.MedicineId
INNER JOIN Customers c ON s.CustomerId = c.CustomerId
ORDER BY s.SaleDate DESC;
GO

-- Low stock medicines (Quantity <= 10)
SELECT MedicineId, MedicineName, Quantity, ExpiryDate
FROM Medicines
WHERE Quantity <= 10
ORDER BY Quantity ASC;
GO

-- Expired medicines
SELECT MedicineId, MedicineName, Quantity, ExpiryDate
FROM Medicines
WHERE ExpiryDate < GETDATE()
ORDER BY ExpiryDate ASC;
GO

PRINT 'PharmacyDB setup complete!';
GO

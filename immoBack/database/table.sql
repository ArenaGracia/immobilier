CREATE DATABASE immo_base;
\c immo_base;

CREATE TABLE Role(
    id_role SERIAL PRIMARY key,
    intitule VARCHAR(200)
);

CREATE TABLE Utilisateur(
    id_utilisateur SERIAL PRIMARY KEY,
    nom VARCHAR(200),
    email VARCHAR(200),
    mdp VARCHAR(200),
    contact VARCHAR(200),
    id_role INTEGER REFERENCES Role(id_role)
);

CREATE TABLE Type_bien(
    id_type SERIAL PRIMARY KEY,
    nom VARCHAR(200),
    commission DECIMAL(10,2)
);

CREATE TABLE region(
    id_region SERIAL PRIMARY KEY,
    nom VARCHAR(200)
);

CREATE TABLE Bien(
    id_bien SERIAL PRIMARY KEY,
    reference VARCHAR(20),
    nom VARCHAR(200),
    description TEXT,
    id_region INTEGER REFERENCES Region(id_region),
    loyer_mensuel DECIMAL(20,2),
    id_type INTEGER REFERENCES Type_bien(id_type),
    id_proprietaire INTEGER REFERENCES Utilisateur(id_utilisateur)
);

CREATE TABLE Location(
    id_location SERIAL PRIMARY KEY,
    id_client INTEGER REFERENCES Utilisateur(id_utilisateur),
    id_bien INTEGER REFERENCES bien(id_bien),
    duree INTEGER,
    debut DATE,
    loyer_mensuel DECIMAL(20,2),
    commission DECIMAL(10,2)
);

CREATE TABLE token_utilisateur(
    id_utilisateur INTEGER REFERENCES Utilisateur(id_utilisateur),
    valeur TEXT,
    date_expiration TIMESTAMP
);

CREATE TABLE mois (
    numero INT PRIMARY KEY,
    nom VARCHAR(20)
);

INSERT INTO mois (numero, nom) VALUES
(1, 'janvier'),
(2, 'février'),
(3, 'mars'),
(4, 'avril'),
(5, 'mai'),
(6, 'juin'),
(7, 'juillet'),
(8, 'août'),
(9, 'septembre'),
(10, 'octobre'),
(11, 'novembre'),
(12, 'décembre');



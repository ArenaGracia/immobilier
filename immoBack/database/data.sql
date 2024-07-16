INSERT INTO Role(intitule) VALUES ('admin'),('propriétaire'),('client');
INSERT INTO Utilisateur(nom,email,mdp,id_role) VALUES ('Arena','arena@gmail.com','arena',1);

INSERT INTO Utilisateur(contact,id_role) VALUES ('0340089076',2),('0327790045',2);
INSERT INTO Utilisateur(contact,id_role) VALUES ('0331178009',2);

INSERT INTO Utilisateur(email,id_role) VALUES 
    ('rakoto@gmail.com',3),
    ('rajao@yahoo.com',3),
    ('societe@gmail.com',3),
    ('entreprise@gmail.com',3);

INSERT INTO Region(nom) VALUES
    ('Boeny'),('Analamanga'),('Vakinankaratra');

INSERT INTO type_bien(nom,commission) VALUES 
    ('Villa',8.5),
    ('Maison',9),
    ('Immeuble',6);

INSERT INTO Bien(reference,nom,description,id_region,loyer_mensuel,id_type,id_proprietaire) VALUES
    ('V110','Villa luxe en bord de mer','Magnifique villa de 250 m² avec vue imprenable sur la mer. Elle comprend 5 chambres, un grand séjour, une cuisine haut de gamme, et une piscine à débordement. Accès direct à la plage.',1,1890000,1,2),

    ('V130','Villa Provençale avec Jardin','Dans un cadre enchanteur, cette villa de 200 m² propose 4 chambres, un salon spacieux, une cuisine équipée, et un jardin de 1000 m² avec arbre fruitier. Un havre de paix',2,2500000,1,3),

    ('M340','Maison Contemporaine en Ville','Découvrez cette maison de ville de 150 m², entièrement rénovée avec des matériaux de qualité. Elle comprend 3 chambres, un vaste séjour avec baies vitrées, une cuisine ouverte, et une terrasse ensoleillée. Proche des commerces et transports.',2,2200000,2,2),

    ('I003','Immeuble Neuf avec Commerces au Rez-de-Chaussée','Immeuble moderne de 700 m², comprenant 6 appartements de haut standing et 2 commerces au rez-de-chaussée. Construction récente avec matériaux de qualité. Emplacement stratégique.',3,4570000,3,4);

INSERT INTO Location(id_bien,id_client,duree,debut) VALUES 
    (1,5,8,'2023-02-10'),
    (1,6,1,'2023-01-07'),
    (3,7,12,'2023-02-08'),
    (3,8,24,'2024-04-10');



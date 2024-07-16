CREATE OR REPLACE VIEW v_utilisateur AS(
    SELECT u.*,r.intitule
        FROM Utilisateur u  
        JOIN Role r ON r.id_role=u.id_role
);

CREATE OR REPLACE VIEW v_bien AS(
    SELECT*FROM bien
);

CREATE OR REPLACE VIEW v_location AS(
    SELECT*FROM location
);

CREATE OR REPLACE VIEW v_bien_libcomplet AS(
    SELECT v.*, t.nom type, commission
        FROM v_bien v 
        JOIN type_bien t ON t.id_type=v.id_type
);

CREATE OR REPLACE VIEW v_location_chiffre_gain1  AS(
    SELECT *, (loyer_mensuel+((loyer_mensuel*commission)/100::DECIMAL(20,2))::DECIMAL(20,2)) chiffre_affaire, ((loyer_mensuel*commission)/100::DECIMAL(20,2))::DECIMAL(20,2) gain
        FROM v_location
);

CREATE OR REPLACE VIEW v_location_chiffre_gain AS(
    WITH RECURSIVE generate_series AS (
        SELECT 0 AS n
        UNION ALL
        SELECT n + 1
        FROM generate_series
        WHERE n < (SELECT MAX(duree) FROM location) - 1
    )
    SELECT
        l.id_location,
        l.id_client,
        l.id_bien,
        (l.debut + (interval '1 month' * gs.n))::date AS date,
        l.loyer_mensuel,
        l.commission,
        l.chiffre_affaire,
        l.gain
    FROM
        v_location_chiffre_gain1 l,
        generate_series gs
    WHERE
        gs.n < l.duree
);

CREATE OR REPLACE VIEW v_annee AS(
    SELECT EXTRACT(YEAR FROM date) an 
        FROM v_location_chiffre_gain
    GROUP BY an 
);

CREATE or REPLACE VIEW v_mois AS(
    SELECT 0 AS n
        UNION ALL
        SELECT n + 1
        FROM generate_series
    WHERE n <= 12
);

CREATE OR REPLACE VIEW v_location_chiffre_gain2 AS(
    SELECT EXTRACT(MONTH FROM date) mois, EXTRACT(YEAR FROM date) an, chiffre_affaire, gain
        FROM v_location_chiffre_gain v
);

CREATE OR REPLACE VIEW v_location_chiffre_gain3 AS(
    SELECT mois, an, SUM(chiffre_affaire) chiffre_affaire, SUM(gain) gain
        FROM v_location_chiffre_gain2 v
    GROUP BY mois,an
);

CREATE or REPLACE VIEW v_chiffre_proprietaire AS(
    SELECT v.*,id_proprietaire
        FROM v_location_chiffre_gain v 
        JOIN v_bien b ON b.id_bien=v.id_bien
);

CREATE OR REPLACE VIEW v_chiffre_proprietaire1 AS(
    SELECT id_proprietaire,EXTRACT(MONTH FROM date) mois, EXTRACT(YEAR FROM date) an, chiffre_affaire, gain
        FROM v_chiffre_proprietaire v
);

CREATE OR REPLACE VIEW v_chiffre_proprietaire2 AS(
    SELECT id_proprietaire,mois, an, SUM(chiffre_affaire) chiffre_affaire, SUM(gain) gain
        FROM v_chiffre_proprietaire1 v
    GROUP BY id_proprietaire,mois,an
);

SELECT * FROM 

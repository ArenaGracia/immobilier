CREATE OR REPLACE VIEW get_location_mois AS(
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
        l.commission
    FROM
        location l,
        generate_series gs
    WHERE
        gs.n < l.duree
);

CREATE OR REPLACE FUNCTION get_location_mois(debut DATE,fin DATE) 
RETURNS TABLE(
    mois VARCHAR,
    an INTEGER,
    chiffre_affaire NUMERIC,
    gain NUMERIC
) AS $$
DECLARE
    mois_debut INTEGER;
    mois_fin INTEGER;
    an_debut INTEGER;
    an_fin INTEGER;
BEGIN
    mois_debut:= EXTRACT(MONTH FROM debut);
    an_debut:= EXTRACT(YEAR FROM debut);
    mois_fin:= EXTRACT(MONTH FROM fin);
    an_fin:= EXTRACT(YEAR FROM fin);

    RETURN QUERY
    SELECT m.nom,CAST(v.an AS INTEGER),v.chiffre_affaire,v.gain
        FROM v_location_chiffre_gain3 v
        JOIN mois m ON m.numero=v.mois
    WHERE v.mois BETWEEN mois_debut AND mois_fin AND v.an BETWEEN an_debut AND an_fin;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_chiffre_proprietaire(debut DATE,fin DATE,proprietaire INTEGER)
RETURNS NUMERIC
AS $$
DECLARE
    mois_debut NUMERIC;
    mois_fin NUMERIC;
    an_debut NUMERIC;
    an_fin NUMERIC;
    chiffre_affaire NUMERIC := 0;
BEGIN
    mois_debut:= EXTRACT(MONTH FROM debut);
    an_debut:= EXTRACT(YEAR FROM debut);
    mois_fin:= EXTRACT(MONTH FROM fin);
    an_fin:= EXTRACT(YEAR FROM fin);

    SELECT SUM(v.chiffre_affaire) INTO chiffre_affaire
        FROM v_chiffre_proprietaire2 v
    WHERE v.mois BETWEEN mois_debut AND mois_fin 
        AND v.an BETWEEN an_debut AND an_fin 
        AND v.id_proprietaire=proprietaire;

    RETURN chiffre_affaire;
END;
$$ LANGUAGE plpgsql;

SELECT*FROM get_location_mois('2023-01-02','2023-09-30');
SELECT*FROM get_chiffre_proprietaire('2023-01-02','2023-09-30',2);





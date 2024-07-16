CREATE OR REPLACE FUNCTION insert_detail_bien()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE Location 
        SET loyer_mensuel=(
            SELECT loyer_mensuel FROM bien WHERE id_bien=NEW.id_bien
        ), commission=(
            SELECT commission from v_bien_libcomplet WHERE id_bien=NEW.id_bien
        )
    WHERE id_location=NEW.id_location;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER insert_location_bien
AFTER INSERT ON location
FOR EACH ROW
EXECUTE FUNCTION insert_detail_bien();
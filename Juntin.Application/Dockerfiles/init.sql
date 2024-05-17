DO
$$
BEGIN
   IF NOT EXISTS (SELECT FROM pg_catalog.pg_database WHERE datname = 'juntin') THEN
      CREATE DATABASE juntin;
   END IF;
END
$$;
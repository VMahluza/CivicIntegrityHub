-- Runs automatically on first container init
USE civic_integrity_hub_db;

-- example table
CREATE TABLE IF NOT EXISTS sample_entity (
  id BIGINT PRIMARY KEY AUTO_INCREMENT,
  name VARCHAR(200) NOT NULL,
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

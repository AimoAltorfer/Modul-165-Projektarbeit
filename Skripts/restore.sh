#!/bin/bash

# Universal Restore-Verzeichnis
backup_path="/opt/backups"

# Datenbankname
db_name="SkiServiceManagement"

# Datum des Backups, das wiederhergestellt werden soll
backup_date="2024-02-02" # Ändern Sie das Datum entsprechend Ihrem Backup

# Restore-Kommando
mongorestore --db $db_name $backup_path/$db_name-$backup_date
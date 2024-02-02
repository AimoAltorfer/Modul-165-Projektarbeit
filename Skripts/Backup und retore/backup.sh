#!/bin/bash

# Universal Backup-Verzeichnis
backup_path="/opt/backups"

# Datenbankname
db_name="SkiServiceManagement"

# Erstellen des Backup-Verzeichnisses falls es nicht existiert
mkdir -p $backup_path

# Datum formatieren für den Dateinamen
date=$(date +%F)

# Backup-Kommando
mongodump --db $db_name --out $backup_path/$db_name-$date

# Optional: Komprimieren des Backup in ein Archiv
tar -czvf $backup_path/$db_name-$date.tar.gz -C $backup_path $db_name-$date

# Altes Backup-Verzeichnis löschen
rm -rf $backup_path/$db_name-$date
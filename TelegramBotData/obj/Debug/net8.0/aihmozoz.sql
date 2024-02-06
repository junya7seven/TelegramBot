CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "UserInfo&Messages" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_UserInfo&Messages" PRIMARY KEY AUTOINCREMENT,
    "ChatId" INTEGER NOT NULL,
    "UserName" TEXT NULL,
    "FirstName" TEXT NULL,
    "LastName" TEXT NULL,
    "Bio" TEXT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240205224234_Install', '8.0.1');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "UserInfo&Messages" ADD "Message" TEXT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240205224629_AddNewColumMessage', '8.0.1');

COMMIT;


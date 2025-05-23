﻿EXECUTE BLOCK
AS
BEGIN
	BEGIN
		EXECUTE STATEMENT
			_UTF8'CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" VARCHAR(150) NOT NULL,
    "ProductVersion" VARCHAR(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);
';
		WHEN SQLSTATE '42S01' DO
		BEGIN
		END
	END
END
START TRANSACTION;
CREATE TABLE "Challenges" (
    "Id" INTEGER GENERATED BY DEFAULT AS IDENTITY NOT NULL,
    "Title" VARCHAR(255) NOT NULL,
    CONSTRAINT "PK_Challenges" PRIMARY KEY ("Id")
);

CREATE TABLE "PlayerScores" (
    "Id" INTEGER GENERATED BY DEFAULT AS IDENTITY NOT NULL,
    "Name" VARCHAR(6) NOT NULL,
    "Score" INTEGER NOT NULL,
    CONSTRAINT "PK_PlayerScores" PRIMARY KEY ("Id")
);

CREATE TABLE "ChallengeOption" (
    "Id" INTEGER GENERATED BY DEFAULT AS IDENTITY NOT NULL,
    "Action" VARCHAR(50) NOT NULL,
    "ChallengeId" INTEGER NOT NULL,
    CONSTRAINT "PK_ChallengeOption" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ChallengeOption_Challenges_~" FOREIGN KEY ("ChallengeId") REFERENCES "Challenges" ("Id") ON UPDATE NO ACTION ON DELETE CASCADE
);

CREATE TABLE "ChallengeOptionConsequence" (
    "Id" INTEGER GENERATED BY DEFAULT AS IDENTITY NOT NULL,
    "Answer" VARCHAR(100) NOT NULL,
    "Consequence" VARCHAR(255) NOT NULL,
    "Health" INTEGER,
    "Money" INTEGER,
    "Power" INTEGER,
    "ChallengeOptionId" INTEGER NOT NULL,
    CONSTRAINT "PK_ChallengeOptionConsequence" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ChallengeOptionConsequence_~" FOREIGN KEY ("ChallengeOptionId") REFERENCES "ChallengeOption" ("Id") ON UPDATE NO ACTION ON DELETE CASCADE
);

CREATE INDEX "IX_ChallengeOption_ChallengeId" ON "ChallengeOption" ("ChallengeId");

CREATE INDEX "IX_ChallengeOptionConsequence_~" ON "ChallengeOptionConsequence" ("ChallengeOptionId");

CREATE UNIQUE INDEX "IX_PlayerScores_Name" ON "PlayerScores" ("Name");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES (_UTF8'20250208125728_First Migration', _UTF8'9.0.1');

ALTER TABLE "PlayerScores" ADD "GameStateToken" CHAR(36) DEFAULT _UTF8'' NOT NULL;

CREATE TABLE "GameStates" (
    "Token" CHAR(36) NOT NULL,
    "Health" INTEGER NOT NULL,
    "Money" INTEGER NOT NULL,
    "Power" INTEGER NOT NULL,
    "Score" INTEGER NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "EndedAt" TIMESTAMP,
    CONSTRAINT "PK_GameStates" PRIMARY KEY ("Token")
);

CREATE INDEX "IX_PlayerScores_GameStateToken" ON "PlayerScores" ("GameStateToken");

ALTER TABLE "PlayerScores" ADD CONSTRAINT "FK_PlayerScores_GameStates_Gam~" FOREIGN KEY ("GameStateToken") REFERENCES "GameStates" ("Token") ON UPDATE NO ACTION ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES (_UTF8'20250216205058_GameState_PlayerScoreAddedRelation', _UTF8'9.0.1');

COMMIT;


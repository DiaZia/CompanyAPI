# REST API pre Organizačnú štruktúru firmy a evidenciu zamestnancov

Tento projekt poskytuje REST API na efektívne spravovanie organizačnej štruktúry firiem a evidenciu ich zamestnancov. 
API umožňuje vytvárať, upravovať a mazať až štvorúrovňovú hierarchickú organizačnú štruktúru. 

## Návod na spustenie:

Stiahnite všetky súbory tohto projektu a otvorte súbor s príponou *.sln* v prostredí **Microsoft Visual Studio**.
Vytvorte si databázu pomocou nástroja SQL Server Management Studio a spustite v nej skript nachádzajúci sa v priečinku *SQL*. 
Pre úspešné vytvorenie databázy bude potrebné mať nainštalovaný SQL Server Express a vytvoriť lokálny server.
Do súboru appsettings.json v kóde projektu pridajte príslušný ***Connection String*** k vašej databáze.
Spustite program v Microsoft Visual Studio. Po spustení sa automaticky otvorí generovaná dokumentácia API pomocou nástroja Swagger.
V dokumentácii Swaggera budete mať možnosť pridávať, upravovať a mazať jednotlivé prvky organizačnej štruktúry. Všetky zmeny a údaje sa budú ukladať do databázy.
Týmto spôsobom môžete efektívne pracovať s organizačnou štruktúrou firiem a evidenciou zamestnancov

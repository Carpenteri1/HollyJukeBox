[![.NET](https://github.com/Carpenteri1/HollyJukeBox/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Carpenteri1/HollyJukeBox/actions/workflows/dotnet.yml)
[![CodeQL Advanced](https://github.com/Carpenteri1/HollyJukeBox/actions/workflows/codeql.yml/badge.svg)](https://github.com/Carpenteri1/HollyJukeBox/actions/workflows/codeql.yml)

# Holly JukeBox

### Starta
-  git clone https://github.com/Carpenteri1/HollyJukeBox.git eller ladda ner som zip
-  unzip (skip if git clone)
-  cd HollyJukeBox
-  dotnet restore
-  dotnet build
-  dotnet run
### Installera self contained
Om man vill köra HollyJukeBox helt self containd.
Efter att ha gjort någon av commandon nedanför hiitar du allt i HollyJukeBox/bin/Release/net8.0/osx-arm64/publish/

#### Windows (x64)
- dotnet publish -c Release -r win-x64 --self-contained true
#### Linux (x64)
- dotnet publish -c Release -r linux-x64 --self-contained true
#### macOS (x64)
- dotnet publish -c Release -r osx-x64 --self-contained true
#### macOS (Apple Silicon)
- dotnet publish -c Release -r osx-arm64 --self-contained true
### Ramverk
- NET Core 8
- ADO.NET
- ASP.NET
- C#
- Swagger
- Dapper
- MediaR
- SQLlite
- Poppin
- Yaml
#### .NET Core
Jag har valt att använda .NET 8, som är den senaste LTS-versionen (Long Term Support). Det erbjuder hög stabilitet, långsiktigt stöd och god prestanda.

.NET är mitt huvudsakliga utvecklingsspråk, vilket gör det till ett naturligt val. Plattformen är väletablerad, kraftfull och lätt att skala upp till större system.

För denna lösning var det självklart att bygga ett REST API med ASP.NET Core, eftersom ramverket erbjuder en mängd funktionalitet “out of the box”, såsom:
- Inbyggd routing
- Swagger-stöd
- Dependency Injection
- Middleware för t.ex. logging och felhantering

Valet av ADO.NET och Dapper som datalager följer .NET:s naturliga arkitekturella styrkor när man vill ha kontroll, prestanda och låg overhead.
#### Swagger
Swagger ingår som standard när man skapar ett API-projekt i .NET Core och är ett mycket smidigt verktyg för att testa, dokumentera och visualisera API:er.

Det möjliggör en interaktiv dokumentation där man kan skicka anrop direkt via webbläsaren, vilket underlättar både utveckling och felsökning. Swagger är väl etablerat i branschen, enkelt att konfigurera, och ger snabbt värde i ett proof of concept.

Jag är medveten om att Swagger byts ut i vissa sammanhang i nyare versioner av .NET, men valde ändå att använda det eftersom:
- Det är beprövat och välkänt
- Det finns god verktygssupport
- Jag har personlig erfarenhet och kännedom om det
#### Dapper
Jag har valt att använda Dapper som datalager istället för att arbeta direkt med ADO.NET. Dapper är ett så kallat micro ORM som kombinerar hög prestanda med låg komplexitet. Det erbjuder en mycket lättviktig och snabb datatillgång, utan att abstrahera bort SQL – vilket passar utmärkt för ett proof of concept där kontroll och läsbarhet är prioriterade.

Fördelarna med Dapper i detta sammanhang:
- Minimal kodmängd
- Hög prestanda, nära rå ADO.NET
- Enkel syntax och snabb inlärningströskel
- Tydlig separation av queries i kodbasen

Valet stödjer också en modulär struktur där varje query eller kommando är explicit och lätt att underhålla.
#### MediaR
För att strukturera applikationslogiken har jag valt att använda MediatR tillsammans med CQRS (Command Query Responsibility Segregation).

MediatR fungerar som ett mediator pattern-bibliotek, vilket ger en tydlig separation mellan API-lagret (controllers) och affärslogiken (handlers). Detta möjliggör:
- Lös koppling mellan komponenter
- Enkel testbarhet
- Skalbar struktur vid växande kodbas
- Möjlighet att införa pipeline behaviors för t.ex. loggning, validering, caching

Jag använder MediatR främst för queries, och strukturen gör det enkelt att skala vidare och bygga ut funktionalitet utan att controller-lagret växer okontrollerat. Jag är medveten om att det kan upplevas som överengineering i enklare lösningar, men ser det som en strategisk investering i struktur.
#### SQLite
För datalagring använder projektet SQLite, en lättviktig, filbaserad databas som kräver minimal konfiguration. Den lämpar sig utmärkt för ett proof of concept då den:
- Kräver ingen separat server
- Är portabel och fungerar direkt på alla plattformar
- Har fullständig SQL-stöd
- Passar väl för lokal utveckling och snabb testning

SQLite är ett bra val i detta sammanhang eftersom det gör setup och distribution enkel, samtidigt som det tillåter ett tydligt schema och persistent lagring.
#### In-Memory Caching
Projektet använder Microsoft.Extensions.Caching.Memory för inbyggd in-memory caching i .NET. Det möjliggör temporär lagring av data direkt i applikationens minne, vilket är särskilt användbart för att:
- Minska onödiga externa anrop
- Snabba upp svarstider
- Förbättra resurseffektivitet

Cachen används exempelvis för att spara artistinformation som hämtas via externa källor. Genom att använda IMemoryCache med konfigurerad expiration (t.ex. sliding eller absolute), undviker vi upprepade nätverksanrop och förbättrar användarupplevelsen i klienten.

Denna typ av caching lämpar sig mycket väl för API-tjänster där datan är relativt statisk under en kortare tid, och den är enkel att konfigurera och underhålla.
#### Poppin
Poppin används som ett komplement till MediatR för att ge stöd för pipelines och policy-baserade behaviors. Det hjälper till att separera tvärgående aspekter såsom:
- Logging
- Retry
- Timeout-hantering
- Circuit breaker-mönster

Detta följer principen om Separation of Concerns och gör systemet mer modulärt och underhållbart. Poppin passar särskilt bra i CQRS-arkitektur där man vill dekorera Handlers med extra funktionalitet utan att blanda in det i affärslogiken.
#### YAML
Projektet använder YAML som format för konfiguration och exempeldata. YAML är ett lättläst, human-friendly format som lämpar sig väl för:
- Enkel strukturering av konfigurationsfiler
- Överskådlig initdata till t.ex. API:er eller tester
- Miljöspecifika inställningar i DevOps

I detta projekt används YAML t.ex. för att definiera seed-data eller inställningar på ett överskådligt sätt. Det ger bättre läsbarhet än JSON i många konfigurationssammanhang.
#### Arkitektur
Projektet följer en förenklad version av Clean Architecture för att uppnå tydlig separation av ansvar och god testbarhet:
- API-lager: Innehåller Controllers som exponerar endpoints via ASP.NET Core.
- Application-lager: Använder CQRS-mönstret med Commands, Queries och Handlers via MediatR för att kapsla in affärslogik.
- Infrastruktur-lager: Innehåller implementeringar för dataåtkomst med Dapper och SQLite som databasmotor.
#### Felhantering
Nuvarande implementation har grundläggande felhantering, men det finns förbättringspotential:
- Vissa null-fall hanteras inte uttryckligen.
- Endpoints saknar tydlig hantering av statuskoder — man förutsätter exempelvis 200 OK, vilket kan ge felaktig responslogik vid t.ex. 404 Not Found eller 400 Bad Request.
- Felhantering i t.ex. repository-lagret är minimal och bör förstärkas.
- Inga enhetstester har ännu skrivits, vilket var planerat men prioriterades bort på grund av tidsramen.

I en vidareutveckling bör global felhantering via ExceptionMiddleware och användning av ProblemDetails enligt RFC7807 övervägas.
#### Kompletterande ändringar ej pushade
Den aktuella branchen är inte helt fullt committad då vissa ändringar gjordes efter att jag tillfälligt förlorade åtkomsten till GitHub. Lokala uppdateringar finns kvar men hann inte pushas innan deadline. Funktionaliteten påverkas inte i grunden, men några förbättringar och justeringar syns inte i det publika repot. Allt ska pushas remote när tillgången till github är tillbaka.
#### Vidareutvecklingsförslag
Det finns flera naturliga utvecklingsvägar för att bygga vidare på detta proof of concept:
- Tester: Lägg till enhetstester för både application-lagret (handlers) och API-lagret (controllers).
- Utökade endpoints: Exempelvis en sökfunktion baserad på artistnamn istället för endast ID. CQRS-strukturen gör det enkelt att införa fler queries med olika filter.
- Flera retry-policies: Just nu används en enkel retry-policy i ArtistInfoHandler.cs. Detta kan generaliseras med Polly eller liknande bibliotek för att hantera instabil extern datahämtning.
- Prestandaförbättringar: ArtistInfo-sökningen kan optimeras – just nu är svarstiden något lång innan all metadata laddats.
- UI-klient: Projektet skulle med fördel kunna kompletteras med en frontend (t.ex. React, Vue eller Blazor) för att visualisera artistdata och albumomslag på ett mer användarvänligt sätt.


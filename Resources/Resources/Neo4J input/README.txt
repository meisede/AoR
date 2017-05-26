Ikke bruk notepad for å se innholdet i disse filene :)


Provinces.txt 
	Inneholder deklarasjonene for kartet ++
	Merk at dette er litt stort, og jeg må finne en måte å splitte opp scriptet på. 
	Antakelig kjøres hele scriptet som EN transaksjon mot neo4j, og denne blir nok litt 
	for stor. Hvis du klipper bort alt over linje 900 eller så så skulle det gå an å laste inn 
	dataene gjennom nettleseren.

Queries.txt
	Inneholder eksempler på spørringer i cypher 

Har lagt inn en ny ZIP fil som inneholder et java program for å laste inn data i Neo4J.
Pakk ut zip-filen, og pass på at det ligger en "data" folder ved siden av "load-data.jar". Hvis du har Java runtime installert så er 
det bare å dobbeltklikke på filen for å kjøre den, men du ser bedre hva som foregår hvis du gjør dette fra CMD med følgende kommando:

java -jar load-data.jar


Forutsetter at du har Neo4J installer på localhost med default port. Og at passordet for neo4J er "myneo"

Ta kontakt hvis du har problemer!
Ikke bruk notepad for � se innholdet i disse filene :)


Provinces.txt 
	Inneholder deklarasjonene for kartet ++
	Merk at dette er litt stort, og jeg m� finne en m�te � splitte opp scriptet p�. 
	Antakelig kj�res hele scriptet som EN transaksjon mot neo4j, og denne blir nok litt 
	for stor. Hvis du klipper bort alt over linje 900 eller s� s� skulle det g� an � laste inn 
	dataene gjennom nettleseren.

Queries.txt
	Inneholder eksempler p� sp�rringer i cypher 

Har lagt inn en ny ZIP fil som inneholder et java program for � laste inn data i Neo4J.
Pakk ut zip-filen, og pass p� at det ligger en "data" folder ved siden av "load-data.jar". Hvis du har Java runtime installert s� er 
det bare � dobbeltklikke p� filen for � kj�re den, men du ser bedre hva som foreg�r hvis du gj�r dette fra CMD med f�lgende kommando:

java -jar load-data.jar


Forutsetter at du har Neo4J installer p� localhost med default port. Og at passordet for neo4J er "myneo"

Ta kontakt hvis du har problemer!
# Prague-Parking-1.1 Lab 02 

School assignment from Campus Nyköping. 2021-09-27 -> 2021-10-20.
VG uppnått.

Inledning
Kund önskar ett stöd för en parkering vid slottet i Prag.
Parkeringsplatsen är s.k. valet parking. Kunden lämnar nyckel och fordon samt får ett kvitto vilket ger
rätten att hämta ut fordonet. Parkeringsplatsen sköts av "finniga studenter" och pensionärer så
systemet måste vara enkelt

Parkeringsplatsen tar emot bilar och motorcyklar.
I dagsläget hämtas alla fordon ut före 00.00 när parkeringen stänger. Ej uthämtade fordon körs till en
parkering utanför stan och kunderna får betala straffavgift för att få ut sitt fordon. (Hanteras ej av
systemet)

Kundens krav på systemet
● Systemet skall kunna ta emot ett fordon och tala om vilken parkeringsplats den skall köras
till.
● Manuellt flytta ett fordon från en plats till en annan.
● Ta bort fordon vid uthämtning.
● Söka efter fordon.
● Kunden önskar en textbaserad meny
I dagsläget så behövs ingen sparfunktion. Datorn slås på när parkeringsplatsen öppnar och slås av när
man går hem.
Tekniska krav
● All identifiering av fordon sker genom registreringsnummer
● Registreringsnummer är alltid strängar maxlängd 10 tecken.
● På parkeringsplatsen finns 100 parkeringsrutor
● En parkeringsruta kan innehålla
o 1 bil eller
o 1 mc eller
o 2mc eller
o vara tom

Parkeringsrutorna skall hanteras som en endimensionell vektor (array) av strängar. Vektorn skall hantera
100 element. Kundens personal är människor och förväntar sig att platserna numreras 1–100 i in- och
utmatningar i systemet.

Inlämning
Inlämning sker individuellt. Om ni väljer att samarbeta med någon eller några av klasskamraterna är upp
till er (tips: det är oftast en god idé att jobba parvis)
Inlämning görs i lärplattformen. Antingen görs en ZIP-fil av hela er solution i Visual Studio, eller så
bifogar ni en länk till ert repo i GitHub.
Betygskrav
För G
● För betyget G skall alla kraven ovan vara uppfyllda.
● Applikationen skall gå att köra på en dator annan än er egen (dvs på lärarens dator)
● Om det behövs några speciella handgrepp för att köra applikationen (utöver att trycks F5
eller Ctrl-F5 i Visual Studio) så skall dessa dokumenteras. Använd README.MD i GitHub för
ändamålet.


![Screenshot 2022-10-23 124751](https://user-images.githubusercontent.com/90194213/197387914-82afab63-57e8-412b-9529-305e7f722633.png)
![Screenshot 2022-10-23 124808](https://user-images.githubusercontent.com/90194213/197387918-d9708064-2919-49e6-b4b4-3fba3837f2fe.png)

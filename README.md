# Exercici 1

## Com has fet per evitar interbloquejos i que ningú passes fam?

He implementat lock perquè els comensals no puguin utilitzar els utensilis que ja estan en mans d'altres.

També he fet servir TryEnter perquè si no tenen dos palets a la vegada els deixen a la taula, per evitar interbloquejos.

I per últim he afegit una cua amb prioritats perquè els comensals que han sigut els primers a intentar menjar siguin els primers a menjar quan es pugi.

## Dades d’anàlisi

- Quants comensals ha passat fam? I quin temps de mitjana?
  Cap comensal ha passat fam, el programa acaba per un timer que he posat jo. El temmps de mitjana es 2.85 segons.
- Quantes vegades ha menjat cada comensal de mitjana? 12 vegades.
- Record de vegades que ha menjat un mateix comensal: 14 vegades.
- Record de menys vegades que ha menjat un comensal: 11 vegades.

# Exercici 2

## Com has executat les tasques per tal pintar, calcular i escoltar el teclat al mateix temps? Has diferenciat entre programació paral·lela i asíncrona?

He dividit el programa en diferents tasks:

1. HandleInput() -> Una task només per veure els inputs dels teclat que en aquest cas era el moviment del gat i la lletra q per sortir.

2. GenerateAsteroids() -> Generar els asteroides.

3. Start() -> Esta tota la lògica del joc.
   
El paral·lelisme el faig servir amb Task.Run() per a HandleInput i GenerateAsteroids. L'objectiu aquí era que aquestes tasques (una que bloqueja esperant l'usuari i l'altra que té les seves pròpies esperes internes) no interferissin amb el flux principal del joc. 
L'asincronia la faig servir amb async/await dins del bucle principal del joc (Start). Aquí, l'objectiu principal és controlar el ritme del joc i evitar que el bucle principal es bloquegi mentre faig les pauses (Task.Delay). 

# Bibliografia
https://www.luisllamas.es/naudio/ [Cómo procesar ficheros de audio en .NET con NAudio, 11/05/2025] [Luis Llamas, Abril 2023]
https://learn.microsoft.com/es-es/dotnet/api/system.console?view=net-8.0 [Console Clase, 10/05/2025] [Autor anònim]
https://learn.microsoft.com/es-es/dotnet/api/system.consolekey?view=net-7.0 [ConsoleKey Enum, 10/05/2025] [Autor anònim]
https://learn.microsoft.com/es-es/dotnet/api/system.diagnostics.stopwatch?view=net-8.0  [Stopwatch Clase, 06/05/2025] [Autor anònim]
https://learn.microsoft.com/es-es/dotnet/api/system.random?view=net-8.0 [Random Clase, 06/05/2025] [Autor anònim]

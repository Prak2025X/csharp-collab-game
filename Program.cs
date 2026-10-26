using System;

// WSPOLNY KONTRAKT. Wszystkie dane inicjalizujemy przed Start().
const int MAKS_ENERGIA = 10;
int pokoj = 1;
int energia = MAKS_ENERGIA;
bool karta = false;
bool bezpiecznik = false;
bool zasilanie = false;
bool koniec = false;
bool wygrana = false;
int licznikRuchow = 0;

// SEKCJA 0: GOTOWY SILNIK NAUCZYCIELA. Nie zmieniaj w zadaniu podstawowym.
Start();
while (true)
{
    Console.Write("> ");
    string? wejscie = Console.ReadLine();
    if (wejscie == null)
    {
        Console.WriteLine("Koniec wejscia. Zamykam program.");
        break;
    }

    string polecenie = wejscie.Trim().ToLowerInvariant();
    if (polecenie == "koniec")
    {
        Console.WriteLine("Do zobaczenia!");
        break;
    }
    if (polecenie == "")
    {
        Console.WriteLine("Wpisz polecenie albo pomoc.");
        continue;
    }

    // Rozdzielacz dziala od poczatku, zeby zespoly mogly testowac niezaleznie.
    switch (polecenie)
    {
        case "start": Start(); break;
        case "pomoc": Pomoc(); break;
        case "status": Status(); break;
        case "mapa": Mapa(); break;
        case "opis": Rozejrzyj(); break;
        case "lewo": Idz("lewo"); break;
        case "prawo": Idz("prawo"); break;
        case "karta": Akcja("karta"); break;
        case "bezpiecznik": Akcja("bezpiecznik"); break;
        case "napraw": Akcja("napraw"); break;
        case "wyjdz": Akcja("wyjdz"); break;
        default: Console.WriteLine("Nieznane polecenie. Wpisz pomoc."); break;
    }
}

void Start()
{
    pokoj = 1;
    energia = MAKS_ENERGIA;
    karta = false;
    bezpiecznik = false;
    zasilanie = false;
    koniec = false;
    wygrana = false;
    Console.WriteLine("UCIECZKA Z SERWEROWNI: PIATEK, 16:59.");
    Console.WriteLine("Administrator napisal: u mnie dziala. Drzwi twierdza inaczej.");
    Console.WriteLine("Masz " + MAKS_ENERGIA + " energii. Wpisz pomoc. Koniec zamyka program.");
    Rozejrzyj();
}

void ZakonczTure()
{
    energia = energia - 1;
    Console.WriteLine("Pozostala energia: " + energia);
    if (wygrana)
    {
        koniec = true;
        Console.WriteLine("WYGRANA! Drzwi otwarte. Weekend uratowany.");
    }
    else if (energia <= 0)
    {
        koniec = true;
        Console.WriteLine("PRZEGRANA. Energia sie skonczyla. Wpisz start albo koniec.");
    }
}

// Status jest gotowy: sluzy do testow, zanim autor mapy skonczy swoja sekcje.
void Status()
{
    Console.WriteLine("Pokoj: " + pokoj);
    Console.WriteLine("Energia: " + energia + "/" + MAKS_ENERGIA);
    Console.WriteLine($"Pasek energii: { PasekEnergii() }");
    Console.WriteLine($"Liczba Ruchow: { licznikRuchow }");
    Console.WriteLine("Karta: " + karta);
    Console.WriteLine("Bezpiecznik: " + bezpiecznik);
    Console.WriteLine("Zasilanie: " + zasilanie);
    Console.WriteLine("Koniec: " + koniec);
    Console.WriteLine("Wygrana: " + wygrana);
}

// SEKCJA A: INFORMACJE I MAPA. Wlasciciel: osoba A.
string NazwaPokoju(int numer)
{
    switch (numer)
    {
        case 1: { return "Recepcja"; }
        case 2: { return "Magazyn"; }
        case 3: { return "Serwerownia"; }
        case 4: { return "Wyjście"; }
        default: { return "Nieznane pomieszczenie"; }
    }
}

void Pomoc()
{
    Console.WriteLine("pomoc - Polecenia i zasady (0 Energii)");
    Console.WriteLine("status - Dane rozgrywki (0 Energii)");
    Console.WriteLine("mapa - Cztery pokoje i jeden znacznik pozycji (0 Energii)");
    Console.WriteLine("opis - Rozejrzenie się w aktualnym pokoju (0 Energii)");
    Console.WriteLine("lewo, prawo - Ruch o jeden pokój (1 Energia)");
    Console.WriteLine("karta - Próba zabrania karty (1 Energia)");
    Console.WriteLine("bezpiecznik - Próba zabrania bezpiecznika (1 Energia)");
    Console.WriteLine("napraw - Próba naprawy zasilania (1 Energia)");
    Console.WriteLine("wyjdz - Próba otwarcia drzwi (1 Energia)");
    Console.WriteLine("start - Nowa rozgrywka (0 Energii)");
    Console.WriteLine("koniec - Zamknięcie programu (0 Energii)");
    Console.WriteLine("koniec to wylaczenie programu, a wyjdz to czynnosc w grze");
    Console.WriteLine("Tylko udany ruch lub udana akcja kosztuje 1 energię");
    Console.WriteLine("Informacje, pusty Enter, nieznana komenda, ściana, duplikat przedmiotu i odrzucona akcja są bezpłatne");
}

void Mapa()
{
    for (int numer = 1; numer <= 4; numer++)
    {
        Console.Write($"{ numer }. { NazwaPokoju(numer) } - ");
        
        switch (numer)
        {
            case 1: Console.Write(karta ? "Karty nie ma na stole" : "Karta jest na stole");                                                                     break;
            case 2: Console.Write((bezpiecznik || zasilanie) ? "Bezpiecznika nie ma na polce" : "Bezpiecznik jest na polce");                                   break;
            case 3: Console.Write(zasilanie ? "Zasilanie jest wlaczone" : "Zasilanie jest wylaczone");                                                          break;
            case 4: Console.Write($"{ (karta ? "Masz karte" : "Nie masz karty") } i { (zasilanie ? "zasilanie jest wlaczone" : "zasilanie jest wylaczone") }"); break;
        }
        
        if (numer == pokoj) { Console.Write(" <-- jestes tutaj"); }
        Console.Write('\n');
    }
}

void Rozejrzyj()
{
    Console.WriteLine("Jestes w: " + NazwaPokoju(pokoj));
    switch (pokoj)
    {
        case 1:
            {
                if (karta) { Console.WriteLine("Karty nie ma na biurku"); }
                else { Console.WriteLine("Karta jest na biurku"); }
                break;
            }
        case 2:
            {
                if (bezpiecznik) { Console.WriteLine("Bezpiecznika nie ma na polce"); }
                else { Console.WriteLine("Bezpiecznik jest na polce"); }

                break;
            }
        case 3:
            {
                if (zasilanie) { Console.WriteLine("Zasilanie jest wlaczone"); }
                else { Console.WriteLine("Zasilanie jest wylaczone"); }

                break;
            }
        case 4:
            {
                if (karta) { Console.WriteLine("Masz karte"); }
                else { Console.WriteLine("Nie masz karty"); }

                if (zasilanie) { Console.WriteLine("Zasilanie jest wlaczone"); }
                else { Console.WriteLine("Zasilanie jest wylaczone"); }

                break;
            }
    }
}

// SEKCJA B: RUCH. Wlasciciel: osoba B.
void Idz(string kierunek)
{
    if (koniec)
    {
        Console.WriteLine("Koniec gry");
        return;
    }
    
    int nastepnyPokoj = pokoj;
    int nastepnaLiczbaRuchow = licznikRuchow;

    if (kierunek == "prawo")
    { 
        nastepnyPokoj++;
        nastepnaLiczbaRuchow++;
    }
    else if (kierunek == "lewo") 
    { 
        nastepnyPokoj--; 
        nastepnaLiczbaRuchow++;
    }
    else
    {
        Console.WriteLine("Nie udalo sie przejsc do tego pokoju");
        return;
    }
    
    if (nastepnyPokoj < 1 || nastepnyPokoj > 4)
    {
        Console.WriteLine("Nie udalo sie przejsc do tego pokoju");
        return;
    }
    
    pokoj = nastepnyPokoj;
    licznikRuchow = nastepnaLiczbaRuchow;

    Rozejrzyj();
    ZakonczTure();
}

// SEKCJA C: PRZEDMIOTY I WYGRANA. Wlasciciel: osoba C.
void Akcja(string co)
{
    // TODO C1: odrzuc akcje po zakonczeniu gry.
    if (koniec) {
        Console.WriteLine("Gra została już zakończona. Nie można wykonać akcji.");
        return;
    }

    // TODO C2: obsluz karta, bezpiecznik, napraw, wyjdz zgodnie z kontraktem.
    // TODO C3: przy odmowie wypisz powod i wykonaj return.
    if (co == "karta") {
        if (pokoj != 1 || karta) {
            Console.WriteLine("Tutaj nie ma karty do zabrania.");
            return;
        }
        karta = true;
        Console.WriteLine("Zabierasz karte.");
    }

    else if (co == "bezpiecznik") {
        if (pokoj != 2 || bezpiecznik || zasilanie) {
            Console.WriteLine("Nie mozna wlaczyc bezpiecznika.");
            return;
        }
        bezpiecznik = true;
        Console.WriteLine("Wlaczono bezpiecznik.");
    }

    else if (co == "napraw") {
        if (pokoj != 3 || bezpiecznik == false || zasilanie == true) {
            Console.WriteLine("Nie mozna naprawic zasilania.");
            return;
        }
        bezpiecznik = false;
        zasilanie = true;
        Console.WriteLine("Naprawiono zasilanie.");
    }

    else if (co == "wyjdz") {
        if (pokoj != 4 || !karta || !zasilanie) {
            Console.WriteLine("Nie mozesz wyjsc. Potrzebujesz karty i zasilania w pokoju 4.");
            return;
        }
        wygrana = true;
        koniec = true;
        Console.WriteLine("Pomyslnie opuszczono");
    }

    else {
        Console.WriteLine("Nieznana akcja: " + co);
        return;
    }

    // TODO C4: tylko po udanej akcji wywolaj raz ZakonczTure.
    ZakonczTure();
    
    //Console.WriteLine("TODO C: akcja " + co);
}

string PasekEnergii()
{
    string pasek = "";

    for (int i = 0; i < energia; i++) { pasek += '#'; }
    for (int i = 0; i < MAKS_ENERGIA - energia; i++) { pasek += '.'; }

    return pasek;
}
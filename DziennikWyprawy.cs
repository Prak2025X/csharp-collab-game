class DziennikWyprawy {
    private bool[] _odwiedzone = new bool[4];

    private string[] _wpisy = {"", "", ""};

    public int LiczbaWpisow {get; private set;}

    public DziennikWyprawy() {
        Reset();
    }

    public void Reset() {
        for (int i = 0; i < _odwiedzone.Length; i++) {
            _odwiedzone[i] = false;
        }

        _odwiedzone[0] = true;

        for (int i = 0; i <_wpisy.Length; i++) {
            _wpisy[i] = "";
        }

        LiczbaWpisow = 0;
    }

    public void Odwiedz (int numer) {
        if (numer < 1 || numer > 4) {
            return;
        }

        _odwiedzone[numer - 1] = true;
    }

    public bool CzyOdwiedzono (int numer) {
        if (numer < 1 || numer > 4) {
            return false;
        }

        return _odwiedzone[numer - 1];
    }

    public int LiczbaOdwiedzonych {
        get {
            int wynik = 0;

            for (int i = 0; i < _odwiedzone.Length; i++) {
                if (_odwiedzone[i]) {
                    wynik++;
                }
            }

            return wynik;
        }
    }
}
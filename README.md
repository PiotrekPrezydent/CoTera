# === CoTera ===
## _Aplikacja do sprawdzania planu zajęć dla studentów uniwersytetu rzeszowskiego_

### Aplikacja znajduję sie do pobrania za darmo dla:
Android - Google Play - WORK IN PROGRESS
IOS - WORK IN PROGRESS

### Krótki opis aplikacji
Aplikacja CoTera jest non-profit projektem przygotowanym dla wszystkich studentów uniwersytetu rzeszowskiego.
Aplikacja w głównym zamyśle ma pokazywać plan zajęć dla konkretnego wybranego kierunku i grupy tego kierunku z uwzględnieniem rodzaju tygodnia A/B, ponadto po pierwszym uruchomieniu aplikacji i wybraniu swojego roku i grupy aplikacja niepotrzebuje już połączenia z internetem.

### Dodawanie swojego planu zajęć
Z racji tego że aplikacja na ten moment niema dostępu do planu zajęć wszystkich kierunków w odpowiednim formacie, potrzebne jest to aby osoby chętnę do wrzucenia swojego planu zajęć pomogły mi je dodać w jeden z trzech sposobów:

1. Napisać do mnie na Teams: `Piotr Prezydent` | `PP131497` z informacją: jaki to jest kierunek + jaki to jest rok oraz podesłać mi link / zdjęcie swojego aktualnego planu zajęć (Tutaj oczekiwanie na dodanie może być najdłuższe z racji tego że sam ręcznię będę musiał wtedy ten plan zajęć przepisać, niemniej jednak będę się starać aby każdy taki podesłany plan zajęć dodać)

2. Napisać do mnie na Teams: `Piotr Prezydent` | `PP131497` z informacją: jaki to jest kierunek + jaki to jest rok oraz z przepisanym już planem zajęć najlepiej w następującym formacie, gdzie w takim jednym zbiorze jest podany cały dzień (jeden dzień jedna wiadomość), (Tutaj czekanie na dodanie to mniej więcej od kilku minut do końca dnia):
``` 
  "PON": [
        { 
            "nazwa": "Przykładowe zajecia w poniedziałek w tygodniu A oraz B", 
            "godziny": "10:00-11:30",
            "rodzaj" : "Wykład",
            "sala" : "101 B2", 
            "tydzien" : "A+B"
        },
        { 
            "nazwa": "Przykładowe zajecia w poniedziałek w tygodniu A", 
            "godziny": "11:45-13:15",
            "rodzaj" : "Laby",
            "sala" : "101 B2", 
            "tydzien" : "A"
        },
        { 
            "nazwa": "Przykładowe zajecia w poniedziałek w tygodniu B", 
            "godziny": "11:45-13:15",
            "rodzaj" : "Laby",
            "sala" : "101 B2", 
            "tydzien" : "B"
        }
    ],
``` 

3. Utworzyć w repozytorium [https://github.com/PiotrekPrezydent/CoTera] Pull Requesta, gdzie do folderu `PlanyZajec` jest wrzucony folder z twoim rokiem i w tym folderze znajduję się grupa którą chcesz dodać do aplikacji, zalecam sprawdzić pliki `PlanyZajec/PrzykladowyRok/PrzykladowaGrupa.json` aby mnieć jak najlepszy obraz jaki format aplikacja akceptuje (tutaj postaram się zakceptować pull requesta natychmiast jak mi przyjdzie mail).


### Aktualizowanie swojego planu zajęć / prośby o zmiany
Tutaj dokładnie takie same zasady jak u góry najlepiej będzię jak albo zrobisz Pull Requesta albo napiszesz mi w wiadomości na teams `Piotr Prezydent` | `PP131497` z informacją co zmienić / co poprawić.

### FAQ
Q: Czy jest to oficjalna aplikacja uniwersytetu rzeszowskiego
A: Nie, projekt zrobiłem samemu z nudów i nic z niego niemam

Q: Czy mam pewność że w aplikacji nieznajdują się żadne wirusy / niepobiera moich danych
A: Tak, kod źródłowy aplikacji jest dostępny w tym repozytorium i każdy ma wgląd do tego jak ta aplikacja dokładnie działa (nic złego nierobi)

Q: Czy aplikacja potrzebuje połączenia z internetem
A: Tylko do momentu wybrania swojego kierunku i grupy, później aplikacja działa bez internetu

Q: Kiedy mogę się spodziewać dodanie mojego kierunku/grupy
A: Jak dostane wiadomość na teams postaram się o natychmiast poinformować kiedy mniej więcej dodam ten plan zajęć i dam znać kiedy już dodam

Q: Czy aplikacje trzeba często aktualizować
A: Nie, jak już się pojawia aktualizacja to pewnie ja w kodzie zmieniałem jakieś pierdoły lub podrasowałem troche UI, ale główne działanie aplikacji tj. ściąganie planów zajęć i tygodni niejest zależne od wersji (w sumie raz pobrana aplikacja niebędzie już nigdy potrzebować aktualizacji).

Q: Kiedy aplikacja pojawi się na google playu / app store
A: Na google playa postaram się wrzucić do końca tego tygodnia tj. do 12.05.2024, na app store niejestem do końca pewny dużo zależy od tego ile osób będzie chciało z aplikacji korzystać (aby wydać coś na google playa / app stora potrzebna jest wpłata startowa, na google playu niemam z tym problemu bo następny projekt planuje też wydać sobie na androida jednak na app storze ta opłata jest troszkę większa i mniej mi jest na rękę, jednak jak będzie potrzeba to zapłace)

Q: Skąd wiesz który jest teraz tydzień A/B 
A: Korzystałem z tego dokumentu uczelni: [https://www.ur.edu.pl/files/user_directory/899/organizacja%20roku%20akademickiego/Zarz%C4%85dzenie%20nr%2077_2023%20ws.%20organizacji%20roku%20akademickiego%202023-2024.pdf]
Jeżeli pojawią się jakieś zmiany można do mnie napisać na teams, chociaż sam staram się w razie czego porawiać tygodnie na bierząco (nietrzeba do tego aktualizować aplikacji oczywiście)

Q: Jak długo aplikacja będzie wspierana (dodawanie/aktualizacja kierunków/grup określanie tygodnia a/b)
A: Na ten moment jeżeli ktoś oprócz mnie będzie z niej korzystać to do momentu aż ukończę studia tj: 2027r.
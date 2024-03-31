Proiectul ar trebui sa ruleze out-of-box. Adica fara alte setari atat timp cat aveti .NET Core 8.
Daca nu il aveti, puteti deschide proiectul in Visual Studio si va v-a intreba daca vreti sa il instalati. Apoi poate fi folosit si din Rider. 


Daca vreti sa folositi acest proiect ca baza pentru proiectul vostru trebuie sa faceti niste setari.

1. Inlocuiti fisierul example.db cu un db de-al vostru.
2. Ca aplicatia sa vada noul vostru db trebuie schimbat in app.config la connectionString numele db-ului.
3. Atentie, merge doar cu db de sqlite.
4. Acum repositories nu vor mai fi la fel, pentru ca probabil nici numele tabelelor nu vor mai fi la fel, aveti grija sa schimbati si acolo in functie de ce aveti nevoie. 

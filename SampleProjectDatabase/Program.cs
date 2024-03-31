using SampleProjectDatabase.model;
using SampleProjectDatabase.repository;

// avem nevoie de chestia asta ca log4net sa stie sa ia din App.config setarile pentru log4net
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SampleProjectDatabase
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Configurare jurnalizare folosind log4net
			// Ca sa mearga, C# trebuie sa stie ca noi vrem sa facem logging cu ajutorul log4net.
			// Acest proiect foloseste .netCore 8.0. Trebuie sa vedem o versiune de log4net potrivita pentru 
			// acest tip de proiect. Ar trebui sa mearga log4net versiunea 2.0.17. (Momentan e instalata aici)
			
			// App.config este un fisier care se citeste automat de catre aplicatie. Undeva in spate 
			// e un check daca exista fisierul asta si il citeste daca exista.
			
			// In app.config noi avem setat niste configs pentru log4net. Un appender pentru consola
			// si un appender pentru fisier. Este acolo un file value=".." care spune path-ul unde se vor 
			// salva toate logurile. Ala poate fi schimbat. Daca puneti pur si simplu un nume, fisierul
			// va fi salvat unde e si executabilul, deoarece la runtime, cand C# vede doar un nume il salveaza
			// in fisierul unde e curent, acela ar fi bin/Debug/net8.0/. 
			// Fun fact! De multe ori v-am spus sa puneti ../../../ apoi nume fisier. ../ merge cumva cu un folder
			// in spate fata de ce exista acum. Deci ../ -> Debug  ;  ../../ -> bin  ; ../../../ merge direct in
			// folderul SampleProjectDatabase.


			// In functie de baza de date aleasa, avem nevoie si de o librarie care sa poata manipula
			// acest db. Ne vom axa pe SQLite deoarece nu am nevoie de un server ca sa il pot face sa mearga.
			// E foarte light si nu foloseste resurse multe. Perfect pentru un proiect de test.
			// Avem nevoie deci de o librarie care sa manipuleze SQLite. Din nou, folosim .netCore deci avem
			// nevoie de o librarie specifica. Microsoft.data.sqlite 9.0.0 e libraria pe care o putem lua si
			// care sa ne ajute. Dupa ce o instalati veti vedea ca acum aveti acces la o clasa SqliteConnection.


			// Ca sa ne conectam la DB avem nevoie de un url catre baza de date. Am putea sa punem direct
			// aici url catre db. Dar vrem sa facem asta printr-un fisier de configurare. Astfel noi putem, 
			// separa partea de configurari de partea de actual coding. 
			// Tot in App.config avem deja un tag numit "connectionString". Pe baza acestui tag noi putem
			// extrage ce se afla acolo. Folosim un tag numit "add" in care avem un "name" care defapt e un
			// identificator pentru valoarea pe care o avem la "connectionString". Astfel C# ne pune la
			// dispozitie o clasa ConfigurationManager care deja in spate parseaza acest fisier automat si
			// poate citi valorile in functie de ce "nume" ii dam noi. 
			
			// C# are deja din System o serie de interfete ca de exemplu IDbConnection, IDbCommand, etc. noi vrem sa
			// ne folosim de aceste interfete pentru ca ele aduc abstractizarea. Daca schimbam in viitor db-ul, noi
			// va trebui sa schimbam doar crearea conexiunii in clasa de DbConnection, iar restul comenzilor ar trebui
			// sa ramana la fel. De aceea in ClientDbRepository ma folosesc doar de aceste interfete si nu e nimic SQLite
			// specific. 


			Console.WriteLine("Cream repository pentru clienti");
			IClientRepository clientRepository = new ClientDbRepository();

			Console.WriteLine("Clienti initial: ");
			foreach (var client in clientRepository.FindAll())
			{
				Console.WriteLine(client);
			}
			
			
			Console.WriteLine("Dam save la un client");
			var clientSalvat = clientRepository.Save(new Client("Nume" + new Random().Next(1, 100)));
			Console.WriteLine("Am salvat clientul " + clientSalvat);
			
			Console.WriteLine("Clienti finali: ");
			foreach (var client in clientRepository.FindAll())
			{
				Console.WriteLine(client);
			}
		}
	}
}
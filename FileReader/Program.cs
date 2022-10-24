namespace FileReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Skapar sökvägen för lokal textfil.
            string filUtanSyntax = "text.txt";
            string filMedSyntax = "lista.txt";

            //Skapa 2 st filer
            SkapaFil(filUtanSyntax);
            SkapaFil(filMedSyntax);

            //Fyll filer med data
            SkrivNyttInnehållTillFil(filUtanSyntax);
            SkrivTillBefintligtInnehåll(filUtanSyntax);
            SkrivArrayTillBefintligtInnehåll(filUtanSyntax);

            SkrivArrayMedSpecifikSyntax(filMedSyntax);

            //Läs av fil utan syntax
            ReadAllOfFile(filUtanSyntax);
            ReadFileWithStreamReader(filUtanSyntax);

            //Läs av fil med Syntax
            ReadAllOfFileWithSyntax(filMedSyntax);
            CreateObjectsFromFile(filMedSyntax);
        }

        static void SkapaFil(string filePath)
        {
            //Skapa en fil med namnet text.txt, bara om filen INTE existerar
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }
        static void SkrivNyttInnehållTillFil(string filePath)
        {
            //Kontroll för att filen existerar
            if (!File.Exists(filePath))
            {
                //Om filen inte finns, avsluta metoden
                return;
            }

            //Skriva nytt innehåll till filen
            Console.Write("Skriv ett meddelande:");
            string message = Console.ReadLine();

            //Skriv nytt innehåll till textfil som skriver över gammalt innehåll
            File.WriteAllText(filePath, message);
        }
        static void SkrivTillBefintligtInnehåll(string filePath)
        {
            //Kontroll för att filen existerar
            if (!File.Exists(filePath))
            {
                //Om filen inte finns, avsluta metoden
                return;
            }

            //Skriva nytt innehåll till filen
            Console.Write("Skriv ett meddelande:");
            string message = Console.ReadLine();

            //Skriva nytt innehåll till befintligt innehåll
            File.AppendAllText(filePath, message + Environment.NewLine);
        }
        static void SkrivArrayTillBefintligtInnehåll(string filePath)
        {
            //Initera en Array med namn
            String[] names = { "Adam", "Bertil", "Ceasar", "David", "Erik", "Fredrik", "Gustav", "Henrik", "Ivan", "Jakob" };

            //Skriv Arrayen till fil med AppendAllLines
            File.AppendAllLines(filePath, names);
        }
        static void SkrivArrayMedSpecifikSyntax(string filePath)
        {
            //Initera en Array med namn
            String[] names = { "Adam", "Bertil", "Ceasar", "David", "Erik", "Fredrik", "Gustav", "Henrik", "Ivan", "Jakob" };

            //Skriv Arrayen till fil med AppendAllText med en viss syntax
            for (int i = 0; i < names.Length; i++)
            {
                File.AppendAllText(filePath, i.ToString() + ":" + names[i] + Environment.NewLine);
            }
        }
        static void ReadAllOfFile(string filePath)
        {
            //Kontroll för att filen existerar
            if (!File.Exists(filePath))
            {
                //Om filen inte finns, avsluta metoden
                return;
            }

            //Hämtar text från fil och skriver ut i Konsol
            Console.WriteLine(File.ReadAllText(filePath));
        }
        static void ReadAllOfFileWithSyntax(string filePath)
        {
            //Kontroll för att filen existerar
            if (!File.Exists(filePath))
            {
                //Om filen inte finns, avsluta metoden
                return;
            }

            //Hämtar text från fil och sparar det i en array
            string[] answers = File.ReadAllLines(filePath);

            //forEach loop för att skriva ut innehållet
            foreach (string answer in answers)
            {
                //Console.WriteLine( answer );

                //Split på answer
                string[] info = answer.Split(":");
                //info[0] - Nummret
                //info[1] - Namnet

                Console.WriteLine($"Namnet nr {Convert.ToInt32(info[0]) + 1} är {info[1]}");
            }
        }
        static void ReadFileWithStreamReader(string filePath)
        {
            //Kontroll för att filen existerar
            if (!File.Exists(filePath))
            {
                //Om filen inte finns, avsluta programmet
                return;
            }

            //StreamReader för att läsa av innehållet i textfilen
            using (StreamReader file = new StreamReader(filePath))
            {
                //Deklarerar lokala variabler
                int count = 0;
                string line;

                //Så länge det finns rader kvar i filen, fortsätt WHile loopen
                while ((line = file.ReadLine()) != null)
                {
                    //Skriv ut line
                    Console.WriteLine(line);
                    count++;
                }

                //Stänger filen
                file.Close();

                //Skriver ut antalet rader
                Console.WriteLine($"Har skrivit ut {count} antal rader.");
            }
        }
        static void CreateObjectsFromFile(string filePath)
        {
            //Kontroll för att filen existerar
            if (!File.Exists(filePath))
            {
                //Om filen inte finns, avsluta metoden
                return;
            }

            //Skapa en (lokal) lista för PersonObjekt
            List<Person> persons = new List<Person>();
            
            //Öppnar StreamReader
            using (StreamReader file = new StreamReader(filePath))
            {
                //Initiera en String variabel som innehåller den lästa raden
                string line;

                //While loop
                while( (line = file.ReadLine()) != null ) {
                    string[] value = line.Split(":");

                    //Skapa ett objekt av Person, med värden från value, och spara i Lista
                    persons.Add(new Person(value[1], Convert.ToInt32(value[0])));
                }

                //Stänga kopplingen till filen
                file.Close();
            }

            foreach (Person person in persons)
            {
                //Skriv up namnen på varje person
                Console.WriteLine(person.Name);
            }
        }
    }
}
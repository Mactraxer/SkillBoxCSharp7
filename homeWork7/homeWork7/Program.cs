using System;
using System.Collections.Generic;
using System.IO;

namespace homeWork7
{
    class MainClass
    {
        static List<Note> notes;
        static Note currentNote;

        public static void Main(string[] args)
        {
            notes = new List<Note>();
            currentNote = new Note("");
            ShowMainMenu();
            Console.ReadKey();
        }

        static void ShowMainMenu()
        {
            

            const string exitMenuCode = "7";

            string menuCode = "0";

            while (menuCode != exitMenuCode)
            {
                Console.WriteLine("Главное меню");
                Console.WriteLine("1. Добавить новую запись");
                Console.WriteLine("2. Редактировать запись");
                Console.WriteLine("3. Удалить запись");
                Console.WriteLine("4. Вывод записей");
                Console.WriteLine("5. Сохранить записи в файл");
                Console.WriteLine("6. Загрузить записи из файла");
                Console.WriteLine($"{exitMenuCode}. Выход");

                menuCode = Console.ReadLine().ToString();

                switch (menuCode)
                {
                    case "1":
                        ShowMenuAddNote();
                        break;
                    case "2":
                        break;
                    case "3":
                        ShowMenuDeleteNote();
                        break;
                    case "4":
                        ShowPrintNotesMenu();
                        break;
                    case "5":
                        ShowMenuExportNoteToFile();
                        break;
                    case "6":
                        ShowMenuImportNoteFromFile();
                        Console.WriteLine("Записи успешно загружены");
                        break;

                    default:
                        break;
                }
            }
        }

        static void ShowMenuDeleteNote()
        {
            const string exitMenuCode = "5";

            string menuCode = "0";

            while (menuCode != exitMenuCode)
            {
                Console.WriteLine("Меню удаления записей");
                Console.WriteLine("1. Удалить записи по названию");
                Console.WriteLine("2. Удалить запись по номеру");
                Console.WriteLine("3. Удалить запись по тегу");
                Console.WriteLine("4. Удалить все записи");
                Console.WriteLine($"{exitMenuCode}. Выход");

                menuCode = Console.ReadLine().ToString();

                switch (menuCode)
                {
                    case "1":
                        ShowMenuDeleteNoteByName();
                        break;
                    case "2":
                        ShowMenuDeleteNoteById();
                        break;
                    case "3":
                        ShowMenuDeleteNoteByTag();
                        break;
                    case "4":
                        notes.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        static void ShowMenuDeleteNoteByTag()
        {
            Console.WriteLine("Введите тег записи которую хотите удалить");
            string tag = Console.ReadLine().ToString();

            List<Note> notesForDelete = notes.FindAll(note => note.Tags.Contains(tag));

            if (notesForDelete.Count == 0)
            {
                Console.WriteLine($"Записей с тегом {tag} не найдено");
            }
            else
            {
                Console.Write($"Найдено {notesForDelete.Count} записей. Удалить?");
                Console.WriteLine("да/нет");
                string answear = Console.ReadLine().ToString();

                switch (answear)
                {
                    case "да":
                        DeleteNotes(notesForDelete);
                        Console.WriteLine("Записи удалены");
                        break;
                    case "нет":
                        break;
                }
            }
        }
        static void ShowMenuDeleteNoteById()
        {
            Console.WriteLine("Введите номер записи которую хотите удалить");
            string id = Console.ReadLine().ToString();

            List<Note> notesForDelete = notes.FindAll(note => note.Id.ToString() == id);

            if (notesForDelete.Count == 0)
            {
                Console.WriteLine($"Записей с номером {id} не найдено");
            }
            else
            {
                Console.Write($"Найдено {notesForDelete.Count} записей. Удалить?");
                Console.WriteLine("да/нет");
                string answear = Console.ReadLine().ToString();

                switch (answear)
                {
                    case "да":
                        DeleteNotes(notesForDelete);
                        Console.WriteLine("Записи удалены");
                        break;
                    case "нет":
                        break;
                }
            }
        }
        static void ShowMenuDeleteNoteByName()
        {
            Console.WriteLine("Введите имя записи которую хотите удалить");
            string name = Console.ReadLine().ToString();

            List<Note> notesForDelete = notes.FindAll(note => note.Name == name);

            if (notesForDelete.Count == 0)
            {
                Console.WriteLine($"Записей с именем {name} не найдено");
            }
            else
            {
                Console.Write($"Найдено {notesForDelete.Count} записей. Удалить?");
                Console.WriteLine("да/нет");
                string answear = Console.ReadLine().ToString();

                switch (answear)
                {
                    case "да":
                        DeleteNotes(notesForDelete);
                        Console.WriteLine("Записи удалены");
                        break;
                    case "нет":
                        break;
                }
            }
        }

        static void DeleteNotes(List<Note> notesForDelete)
        {
            foreach (Note note in notesForDelete)
            {
                notes.Remove(note);
            }
        }

        static void ShowMenuImportNoteFromFile()
        {
            Console.WriteLine("Введите имя файла из котого хотите загрузить записи");
            string inputFileName = Console.ReadLine().ToString();
            if (TryReadFile(inputFileName))
            {
                ImportNotesFromFile(inputFileName);
            }
        }

        static void ShowMenuExportNoteToFile()
        {
            Console.WriteLine("Введите имя файла в который хотите сохранить записи");
            string outputFileName = Console.ReadLine().ToString();
            ExportNotesToFile(outputFileName);
            Console.WriteLine("Записи успешно сохранены в файл!");
        }

        static void ExportNotesToFile(string fileName)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                foreach (Note note in notes)
                {
                    streamWriter.WriteLine(note.ToString());
                }
            }
        }

        static void ImportNotesFromFile(string fileName)
        {
            //Поток для чтения из файла
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string line;
                string[] noteParts;
                notes = new List<Note>();
                //Процесс чтения файла
                while ((line = streamReader.ReadLine()) != null)
                {
                    noteParts = line.Split('\t');

                    List<string> tags = new List<string>();
                    tags.AddRange(noteParts[4].Split(','));
                    Note newNote = new Note(noteParts[0], noteParts[1], DateTime.Parse(noteParts[2]), DateTime.Parse(noteParts[3]), tags);
                    notes.Add(newNote);
                }

            }
        }
        /// <summary>
        /// Метод проверки доступности файла для чтения и его чтение
        /// </summary>
        /// <param name="fileName">Имя файла для чтения</param>
        /// <returns>Статус доступности файла</returns>
        static bool TryReadFile(string fileName)
        {
            bool readResult = false;
            //Получаем информацию по файлу
            FileInfo fileInfo = new FileInfo(fileName);
            //Проверка существует ли такой файл
            if (fileInfo.Exists)
            {
                //Поток для чтения из файла
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    string line;
                    string lines = "";
                    //Процесс чтения файла
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines += line;
                    }

                    if (lines.Length == 0)
                    {
                        Console.WriteLine($"Файл - {fileInfo.FullName}  - пустой");
                    }
                    else
                    {
                        readResult = true;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Не найден файл - {fileInfo.FullName}");

            }

            return readResult;
        }

        static void ShowMenuAddNote()
        {
            const string exitMenuCode = "5";

            string menuCode = "0";

            while (menuCode != exitMenuCode)
            {
                Console.WriteLine("Меню добавление записи");
                Console.WriteLine("1. Имя заметки");
                Console.WriteLine("2. Описание заметки");
                Console.WriteLine("3. Теги");
                Console.WriteLine("4. Сохранить");
                Console.WriteLine("5. Выход");

                menuCode = Console.ReadLine().ToString();

                switch (menuCode)
                {
                    case "1":
                        WriteName();
                        break;
                    case "2":
                        WriteDescription();
                        break;
                    case "3":
                        ShowTagMenu();
                        break;
                    case "4":
                        SaveNote();
                        break;
                    default:
                        break;
                }
            }
        }

        static void WriteName()
        {
            Console.WriteLine("Введите название заметки ");
            string title = Console.ReadLine().ToString();
            currentNote.Name = title;
        }

        static void WriteDescription()
        {
            Console.WriteLine("Введите заметку ");
            string description = Console.ReadLine().ToString();
            currentNote.Description = description;
        }

        static void WriteTag(string tag)
        {
            currentNote.AddTag(tag);
        }

        static void ShowTagMenu()
        {
            const string exitMenuCode = "2";
            string menuCode = "0";

            do
            {
                Console.WriteLine("Введите тег");
                string tag = Console.ReadLine().ToString();
                WriteTag(tag);

                Console.WriteLine("Продолжить вводить теги?");
                Console.WriteLine("1. Да");
                Console.WriteLine("2. Нет");

                menuCode = Console.ReadLine().ToString();


            } while (exitMenuCode != menuCode);

        }

        static void SaveNote()
        {
            notes.Add(currentNote);
            currentNote = new Note("");
            Console.WriteLine("Запись успешно сохранена!");
        }

        static void ShowPrintNotesMenu()
        {
            const string exitMenuCode = "3";

            string menuCode = "0";

            while (menuCode != exitMenuCode)
            {
                Console.WriteLine("Меню вывода записей");
                Console.WriteLine("1. Вывести на экран все записи");
                Console.WriteLine("2. Вывести на экран отсортированные записи");
                Console.WriteLine("3. Выход");

                menuCode = Console.ReadLine().ToString();

                switch (menuCode)
                {
                    case "1":
                        PrintNotes(notes);
                        break;
                    case "2":
                        ShowMenuPrintSortNotes();
                        break;
                    default:
                        break;
                }
            }
        }

        static void ShowMenuPrintSortNotes()
        {
            const string exitMenuCode = "4";

            string menuCode = "0";

            while (menuCode != exitMenuCode)
            {
                Console.WriteLine("Выберите поле по которму будут отсортированы записи");
                Console.WriteLine("1. Название");
                Console.WriteLine("2. Дата создание");
                Console.WriteLine("3. Дата последнего изменения");
                Console.WriteLine("4. Выход");

                menuCode = Console.ReadLine().ToString();

                switch (menuCode)
                {
                    case "1":
                        SortNotesByName();
                        PrintNotes(notes);
                        break;
                    case "2":
                        SortNotesByCreateDate();
                        PrintNotes(notes);
                        break;
                    case "3":
                        SortNotesByLastEditDate();
                        PrintNotes(notes);
                        break;
                    default:
                        break;
                }
            }
        }

        static void SortNotesByName()
        {
            notes.Sort(Note.CompareByName);
        }

        static void SortNotesByCreateDate()
        {
            notes.Sort(Note.CompareByCreateDate);
        }

        static void SortNotesByLastEditDate()
        {
            notes.Sort(Note.CompareByLastEditDate);
        }

        static void PrintNotes(List<Note> notes)
        {
            foreach (Note note in notes)
            {
                note.Print();
            }
        }
    }
}

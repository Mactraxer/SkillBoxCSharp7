using System;
using System.Collections.Generic;

namespace homeWork7
{
    public struct Note
    {
        
        /// Поля
        
        static uint count = 0;
        private uint id;
        private string name;
        private string description;
        private DateTime createDate;
        private DateTime lastEdit;
        private List<string> tags;

        
        /// Свойства
        
        public string Name
        {
            get { return this.name; }
            set { this.name = value; UpdateLastEditTime(); }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; UpdateLastEditTime(); }
        }

        public DateTime CreateDate
        {
            get { return this.createDate; }
        }

        public DateTime LastEdit
        {
            get { return this.lastEdit; }
        }

        public uint Id
        {
            get { return this.id; }
        }

        public List<string> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
        
        /// Методы
        
        private void UpdateLastEditTime() {
            this.lastEdit = DateTime.Now;
        }

        public void AddTag(string tag)
        {
            tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            tags.RemoveAll(tagName => tagName == tag);
        }

        public void Print()
        {
            Console.WriteLine(
                $"№ {this.id}\n " +
                $"Название: {this.name}\n\n " +
                $"Заметка: {this.description}\n\n " +
                $"Теги: {GetTagsNames()}\n " +
                $"Последние изменения: {this.lastEdit}\n"
                );
        }

        public override string ToString()
        {
            return $"{this.name}\t{this.description}\t{this.createDate}\t{this.lastEdit}\t{String.Join(",", this.tags)}";
        }

        private string GetTagsNames()
        {
            string names = String.Join(", ", tags);
            return names;
            
        }

        public static int CompareByName(Note note1, Note note2)
        {
            return note1.name.CompareTo(note2.name);
        }

        public static int CompareByCreateDate(Note note1, Note note2)
        {
            return note1.createDate.CompareTo(note2.createDate);
        }

        public static int CompareByLastEditDate(Note note1, Note note2)
        {
            return note1.lastEdit.CompareTo(note2.lastEdit);
        }

        ///Конструкторы

        public Note(string name)
            : this(
                  name,
                  "",
                  DateTime.Now,
                  DateTime.Now,
                  new List<String>()
                  )
        { }

        public Note(
            string name,
            string description,
            DateTime createDate,
            DateTime lastEdit,
            List<string> tags
            ) : this()
        {
            this.id = Note.count;
            this.name = name;
            this.description = description;
            this.createDate = createDate;
            this.lastEdit = lastEdit;
            this.tags = tags;
            Note.count++;
        }
    }
}

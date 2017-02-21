using System;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using Twill.Storage.Interfaces.Reports;

namespace Twill.Storage.Files.Reports.To
{
    [XmlRoot(ElementName = "TASK")]
    public class Task
    {
        public Task()
        {
        }

        public Task(int id)
        {
            Createdby = string.Empty;
            Id = id;

            var time = DateTime.Now;

            StartDate = time.ToOADate();
            CreationDate = StartDate;

            STARTDATESTRING = time.ToShortDateString();
            CreationDateString = $"{STARTDATESTRING} {time.ToString("H:mm")}";
        }

        public void Add(string title, double cost, int nextuniqid)
        {
            Tasks.Add(new Task(nextuniqid) { Title = title, Cost = cost });
        }

        [XmlAttribute(AttributeName = "TITLE")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "REFID")]
        public int Refid { get; set; }
        [XmlAttribute(AttributeName = "COMMENTSTYPE")]
        public string CommentStype { get; set; } = "PLAIN_TEXT";
        [XmlAttribute(AttributeName = "CREATEDBY")]
        public string Createdby { get; set; }
        [XmlAttribute(AttributeName = "PRIORITY")]
        public int Priority { get; set; } = 5;
        [XmlAttribute(AttributeName = "RISK")]
        public int Risk { get; set; }
        [XmlAttribute(AttributeName = "COST")]
        public double Cost { get; set; }
        [XmlAttribute(AttributeName = "PERCENTDONE")]
        public string PercentDone { get; set; }
        [XmlAttribute(AttributeName = "STARTDATE")]
        public double StartDate { get; set; }
        [XmlAttribute(AttributeName = "STARTDATESTRING")]
        public string STARTDATESTRING { get; set; }
        [XmlAttribute(AttributeName = "CREATIONDATE")]
        public double CreationDate { get; set; }
        [XmlAttribute(AttributeName = "CREATIONDATESTRING")]
        public string CreationDateString { get; set; }
        [XmlAttribute(AttributeName = "LASTMOD")]
        public double LastMod { get; set; }
        [XmlAttribute(AttributeName = "LASTMODSTRING")]
        public string LastModString { get; set; }
        [XmlAttribute(AttributeName = "CALCCOST")]
        public double CalcCost { get; set; }
        [XmlAttribute(AttributeName = "PRIORITYCOLOR")]
        public string PriorityColor { get; set; } = "15732480";
        [XmlAttribute(AttributeName = "PRIORITYWEBCOLOR")]
        public string PriorityWebColor { get; set; } = "#000FF0";
        [XmlElement(ElementName = "TASK")]
        public List<Task> Tasks { get; set; } = new List<Task>();
         
    }

    [XmlRoot(ElementName = "TODOLIST")]
    public class TodoList<T> : IReportFormatter<T> where T : IReport
    {
        public TodoList()
        {
            FileName = string.Empty;

            UpLastMod();
        }

        public TodoList(string filename)
        {
            FileName = filename;

            UpLastMod();
        }

        private void UpLastMod()
        {
            var time = DateTime.Now;

            LastMod = time.ToOADate();
            LastModString = $"{time.ToShortDateString()} {time.ToString("H:mm")}";
        }

        public void Add(string parenttitle, string title, double cost)
        {
            var lasttask = Tasks.FirstOrDefault(task => task.Title == parenttitle);
            if(lasttask == null)
            { 
                Tasks.Add(lasttask = new Task(NextUniqueid++) { Title = parenttitle }); 
            }

            lasttask.Add(title, cost, NextUniqueid++);

            UpLastMod();
        }

        public string Serialize() => Twill.Tools.Text.XML.Serialization(this);

        public void Add(DayReport<T> dayReport)
        {
            string shortDateString = dayReport.Date.ToShortDateString();

            foreach (var report in dayReport.Reports)
                Add(shortDateString, report.Text, (report.End - report.Start).TotalHours);
        }

        public DayReport<T> Deserialize(string text)
        {
            return null;
        }

        [XmlElement(ElementName = "TASK")]
        public List<Task> Tasks { get; set; } = new List<Task>();
        [XmlAttribute(AttributeName = "PROJECTNAME")]
        public string ProjectName { get; set; } = string.Empty;
        [XmlAttribute(AttributeName = "FILEFORMAT")]
        public int FileFormat { get; set; } = 10;
        [XmlAttribute(AttributeName = "EARLIESTDUEDATE")]
        public double EearliestDueDate { get; set; }
        [XmlAttribute(AttributeName = "LASTMOD")]
        public double LastMod { get; set; }
        [XmlAttribute(AttributeName = "LASTMODSTRING")]
        public string LastModString { get; set; }
        [XmlAttribute(AttributeName = "NEXTUNIQUEID")]
        public int NextUniqueid { get; set; } = 5;
        [XmlAttribute(AttributeName = "FILENAME")]
        public string FileName { get; set; }
        [XmlAttribute(AttributeName = "FILEVERSION")]
        public int FileVersion { get; set; } = 17;
        [XmlAttribute(AttributeName = "APPVER")]
        public string AppVer { get; set; } = "6.8.10.0";
    }
}

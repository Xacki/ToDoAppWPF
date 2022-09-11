using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppWPF.Models;

namespace ToDoAppWPF.Services
{
    internal class FileIOService
    {
        private readonly string _filePath;
        public FileIOService(string filePath)
        {
            _filePath = filePath;
        }

        public BindingList<ToDoModel> LoadData()
        {
            var fileExist = File.Exists(_filePath);
            if (!fileExist)
            {
                File.CreateText(_filePath).Dispose();
                return new BindingList<ToDoModel>();
            }
            using (var reader = File.OpenText(_filePath))
            {
                var fileText = reader.ReadToEnd(); 
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }

        }
        public void SaveData(object toDoDataList)
        {
            using (StreamWriter writer = File.CreateText(_filePath))
            {
                string output = JsonConvert.SerializeObject(toDoDataList);
                writer.Write(output);
            }
        }

    }
}

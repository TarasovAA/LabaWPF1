using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WpfLaba1.Models
{
    class FileMeneger
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);
        public bool isExists(string filePath)
        {
            return File.Exists(filePath);
        }

    }
}

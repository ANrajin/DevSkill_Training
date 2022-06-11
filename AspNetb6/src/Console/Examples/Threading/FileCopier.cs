using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    public class FileCopier
    {
        public event Action<string> OnBegin;
        public event Action<string, string> OnComplete;

        public void Copy(string source, string destination)
        {
            OnBegin($"Started Copy of {source}");

            var ext = Path.GetExtension(source);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var destinationFullPath = Path.Combine(destination, fileName);

            File.Copy(source, destinationFullPath);

            OnComplete($"{source} was copied to {destination}", fileName);
        }
    }
}

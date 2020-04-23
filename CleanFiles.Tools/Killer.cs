using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanFiles.Tools
{
    public class Killer
    {

        private string Path;
        public Killer()
        {
            Path = string.Empty;

        }
        public Killer(string path)
        {
            Path = path;

        }
        public List<string> GetFiles(bool opt = false)
        {

            string root = Path;

            List<string> fileEntries = null;

            if (opt)
            {
                fileEntries = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories).ToList();
            }
            else
            {
                fileEntries = Directory.GetFiles(root).ToList();
            }

            return fileEntries.Select(x => x).ToList();

        }
        public List<FileInfo> GetFilesInfo(bool opt = false)
        {

            string root = Path;

            List<string> fileEntries = null;

            if (opt)
            {
                fileEntries = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories).ToList();
            }
            else
            {
                fileEntries = Directory.GetFiles(root).ToList();
            }

            return fileEntries.Select(x => new FileInfo(x)).ToList();

        }
        public List<FileInfo> EvaluateFileInfoListDuplicated(List<string> list)
        {
            List<FileInfo> result = new List<FileInfo>();
            List<Item> listEnc = new List<Item>();
            list.AsParallel();

            Parallel.ForEach(list, (item) =>
            {

                var hsh = createHashMD5(item);
                listEnc.Add(new Item() { Hash = hsh, Value = item });

            });


            var Repetidos = listEnc.GroupBy(x => x.Hash).Select(t => new { Hash = t.Key, Items = t.Select(o => o.Value).ToList() }).Where(h => h.Items.Count > 1).ToList();

            var ToKillFromItem = Repetidos.Select(j => j.Items.Select(u => new FileInfo(u)).OrderByDescending(ord => ord.CreationTime).Take(j.Items.Count - 1)).ToList().Select(x => x.ToList()).ToList();
            //var ToKillFromItem = Repetidos.Select(j => j.Items.OrderByDescending(ord => ord.Length).Take(j.Items.Count - 1)).ToList().Select(x => x.ToList()).ToList();


            ToKillFromItem.ForEach((item) =>
            {
                result.AddRange(item.Select(x => x));
            });

            return result;
        }
        private string createHashMD5(string cadena)
        {
            string hash = "";

            var stream = File.OpenRead(cadena);

            using (var md5 = MD5.Create())
            {
                hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();

            }
            stream.Close();

            return hash;

        }
        public Item DeleteFileList(List<FileInfo> lista)
        {
            Item item = new Item();
            int count = 0;
            string messageResponse = "";

            lista.ForEach((file) =>
            {

                try
                {
                    if (File.Exists(file.FullName))
                    {
                        File.Delete(file.FullName);

                        count += 1;

                    }

                }
                catch (Exception ex)
                {

                    messageResponse += $"Error  {ex.Message} al eliminar {file} \n";
                }



            });

            return new Item() { Hash = count.ToString(), Value = messageResponse };




        }
    }

    public class Item
    {
        public string Hash { get; set; }
        public string Value { get; set; }
    }
}


using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        string inputFolder = Directory.GetCurrentDirectory();
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found.");
            return;
        }

        long totalMilliseconds = 0;
        foreach (string xmlPath in xmlFiles)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            using (Document pdfDoc = new Document())
            {
                pdfDoc.BindXml(xmlPath);
                string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                pdfDoc.Save(outputFileName);
            }

            sw.Stop();
            long elapsed = sw.ElapsedMilliseconds;
            totalMilliseconds += elapsed;
            Console.WriteLine($"{Path.GetFileName(xmlPath)} -> {Path.GetFileNameWithoutExtension(xmlPath)}.pdf : {elapsed} ms");
        }

        Console.WriteLine($"Processed {xmlFiles.Length} files in {totalMilliseconds} ms. Average: {totalMilliseconds / xmlFiles.Length} ms per file.");
    }
}

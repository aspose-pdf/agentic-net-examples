using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "filled.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                document.Save(memoryStream);
                byte[] pdfBytes = memoryStream.ToArray();
                Console.WriteLine("PDF byte array length: " + pdfBytes.Length);
                // The pdfBytes array can now be sent over a web API.
            }
        }
    }
}
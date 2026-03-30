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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDoc = new Document(inputPath))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                pdfDoc.Save(memoryStream);
                byte[] pdfBytes = memoryStream.ToArray();
                Console.WriteLine($"PDF byte array length: {pdfBytes.Length}");
                // The byte array can now be sent over a web API.
            }
        }
    }
}
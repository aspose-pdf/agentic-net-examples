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

        using (Document doc = new Document(inputPath))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the PDF directly into the memory stream
                doc.Save(memoryStream);

                // Retrieve the byte array from the stream
                byte[] pdfBytes = memoryStream.ToArray();

                // Example usage: output the size of the byte array
                Console.WriteLine($"PDF byte array length: {pdfBytes.Length}");
                // In a real scenario, pdfBytes would be sent over a web API
            }
        }
    }
}
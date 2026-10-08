using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Document.Form holds the AcroForm fields; Count gives the total number
            int fieldCount = doc.Form.Count;
            Console.WriteLine($"Total AcroForm fields: {fieldCount}");
        }
    }
}
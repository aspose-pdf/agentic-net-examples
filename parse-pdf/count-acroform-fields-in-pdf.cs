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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Document.Form may be null if there are no AcroForm fields
            int fieldCount = doc.Form?.Count ?? 0;
            Console.WriteLine($"Total AcroForm fields: {fieldCount}");
        }
    }
}
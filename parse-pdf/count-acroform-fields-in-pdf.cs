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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the total number of AcroForm fields
            int fieldCount = doc.Form.Count;

            Console.WriteLine($"Total AcroForm fields: {fieldCount}");
        }
    }
}
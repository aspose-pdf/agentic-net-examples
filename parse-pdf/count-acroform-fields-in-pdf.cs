using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file whose AcroForm fields we want to count
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Document.Form provides access to the AcroForm; Count returns the total number of fields
            int totalFields = doc.Form.Count;

            // Output the result
            Console.WriteLine($"Total AcroForm fields: {totalFields}");
        }
    }
}
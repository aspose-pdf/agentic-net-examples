using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "processed.pdf";
        const int expectedFieldCount = 5; // set the expected number of form fields

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Verify the number of form fields using Document.Form.Count
            int actualFieldCount = doc.Form.Count;
            Console.WriteLine($"Form fields found: {actualFieldCount}");

            if (actualFieldCount != expectedFieldCount)
            {
                Console.Error.WriteLine($"Unexpected number of form fields. Expected: {expectedFieldCount}, Found: {actualFieldCount}");
                return; // abort processing if the count does not match
            }

            // Place additional processing of the PDF here (e.g., fill fields, flatten, etc.)

            // Save the processed document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
    }
}
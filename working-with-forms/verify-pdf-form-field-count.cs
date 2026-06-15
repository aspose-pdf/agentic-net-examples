using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "processed.pdf";
        const int expectedFormFieldCount = 5; // adjust as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Verify the number of form fields using Document.Form.Count
            int actualCount = doc.Form.Count;
            Console.WriteLine($"Form fields found: {actualCount}");

            if (actualCount != expectedFormFieldCount)
            {
                Console.Error.WriteLine($"Unexpected number of form fields. Expected: {expectedFormFieldCount}, Actual: {actualCount}");
                return; // abort processing
            }

            // ---- Place processing logic here (e.g., fill fields, flatten, etc.) ----
            // Example placeholder: doc.Form.AutoRecalculate = false;
            // --------------------------------------------------------------------

            // Save the modified document (lifecycle rule: Save without extra options writes PDF)
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}
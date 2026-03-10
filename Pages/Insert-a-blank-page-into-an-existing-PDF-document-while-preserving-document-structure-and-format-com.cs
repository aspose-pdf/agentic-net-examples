using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int insertPosition = 2; // Insert after the first page (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Insert an empty page at the desired position.
                // PageCollection.Insert uses 1‑based indexing.
                doc.Pages.Insert(insertPosition);

                // Save the updated document (PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Blank page inserted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set creation/modification dates, and save
        using (Document doc = new Document(inputPath))
        {
            // Set the document creation date to the current UTC time
            doc.Info.CreationDate = DateTime.UtcNow;

            // Set the document modification date to the current UTC time
            doc.Info.ModDate = DateTime.UtcNow;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated timestamps to '{outputPath}'.");
    }
}
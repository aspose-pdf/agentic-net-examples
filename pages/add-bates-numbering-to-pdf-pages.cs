using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to each page
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber = 1000; // start numbering at 1000
                artifact.Suffix = "-";       // dash separator after the number
                // Optional: configure other properties such as alignment, margins, etc.
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied. Output saved to '{outputPath}'.");
    }
}
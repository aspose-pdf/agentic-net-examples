using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string heading = "My Document Heading";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assign the heading as the document title metadata
            doc.SetTitle(heading);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Heading assigned and saved to '{outputPath}'.");
    }
}
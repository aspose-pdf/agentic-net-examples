using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clean_template_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove all form fields from the first page
            doc.Pages[1].Flatten();

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clean template page saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output file name
        const string outputPath = "CenteredTitle.pdf";

        // Ensure the output directory exists (optional)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            doc.Pages.Add();

            // Set window behavior: center the window and display the document title in the title bar
            doc.CenterWindow = true;
            doc.DisplayDocTitle = true;

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created and saved as '{outputPath}'.");
    }
}
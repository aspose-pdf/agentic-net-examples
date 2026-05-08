using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF and ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Add an empty page at the end of the document
            Page separator = doc.Pages.Add();

            // Set the page background to transparent
            separator.Background = Aspose.Pdf.Color.Transparent;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator page added and saved to '{outputPath}'.");
    }
}
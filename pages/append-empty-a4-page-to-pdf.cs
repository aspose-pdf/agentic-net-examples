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

        // Load the existing PDF, modify it, and ensure deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Add an empty page at the end of the document.
            Page newPage = doc.Pages.Add();

            // Resize the newly added page to A4 size.
            newPage.Resize(PageSize.A4);

            // Save the updated document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"A4 page appended and saved to '{outputPath}'.");
    }
}
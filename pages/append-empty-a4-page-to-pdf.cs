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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Append an empty page at the end of the document
            Page newPage = doc.Pages.Add();

            // Resize the newly added page to A4 dimensions
            newPage.Resize(PageSize.A4);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Appended A4 page saved to '{outputPath}'.");
    }
}
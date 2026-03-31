using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "original.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Simple edit: add a new blank page with a text fragment
            Page newPage = document.Pages.Add();
            TextFragment fragment = new TextFragment("Edited copy");
            newPage.Paragraphs.Add(fragment);

            // Save the modified document to a new file, leaving the original untouched
            document.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved as '{outputPath}'. Original remains unchanged.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Aspose.Pdf.Document does NOT expose a Progress event.
        // Therefore we cannot attach a progress handler here.
        // The operation will be performed synchronously.

        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a new page with a text fragment
            Page newPage = doc.Pages.Add();
            TextFragment tf = new TextFragment("Generated page");
            newPage.Paragraphs.Add(tf);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
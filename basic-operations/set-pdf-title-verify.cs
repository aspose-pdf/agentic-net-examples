using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string title = "Sample Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, set its title, and save
        using (Document doc = new Document(inputPath))
        {
            doc.SetTitle(title);
            doc.Save(outputPath);
        }

        // Reopen the saved PDF to verify the title
        using (Document verifyDoc = new Document(outputPath))
        {
            string savedTitle = verifyDoc.Info.Title;
            Console.WriteLine($"Saved title: {savedTitle}");
        }
    }
}
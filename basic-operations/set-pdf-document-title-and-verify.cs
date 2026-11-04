using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle = "My Accessible Document";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, set the document title, and save it
        using (Document doc = new Document(inputPath))
        {
            doc.Info.Title = newTitle;          // Set the title property
            doc.Save(outputPath);               // Save to a new file
        }

        // Reopen the saved PDF and verify the title
        using (Document verifyDoc = new Document(outputPath))
        {
            string savedTitle = verifyDoc.Info.Title ?? string.Empty;
            Console.WriteLine($"Saved title: '{savedTitle}'");
            Console.WriteLine(savedTitle == newTitle
                ? "Title verification succeeded."
                : "Title verification failed.");
        }
    }
}
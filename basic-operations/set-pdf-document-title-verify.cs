using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle = "My New PDF Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, set its title, and save it.
        using (Document doc = new Document(inputPath))
        {
            // Set the document title using the Document.SetTitle method.
            doc.SetTitle(newTitle);
            // Save the modified document.
            doc.Save(outputPath);
        }

        // Re-open the saved PDF to verify that the title was persisted.
        using (Document verifyDoc = new Document(outputPath))
        {
            string savedTitle = verifyDoc.Info.Title;
            Console.WriteLine($"Saved title: \"{savedTitle}\"");
            Console.WriteLine(savedTitle == newTitle
                ? "Title verification succeeded."
                : "Title verification failed.");
        }
    }
}
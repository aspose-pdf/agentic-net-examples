using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle   = "My New Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set its title, and save it
        using (Document doc = new Document(inputPath))
        {
            // Set title via Document.SetTitle method
            doc.SetTitle(newTitle);
            // Also update DocumentInfo.Title for consistency
            doc.Info.Title = newTitle;
            doc.Save(outputPath);
        }

        // Re-open the saved PDF to verify the title
        using (Document verifyDoc = new Document(outputPath))
        {
            string savedTitle = verifyDoc.Info.Title;
            Console.WriteLine($"Saved title: '{savedTitle}'");
            Console.WriteLine(savedTitle == newTitle
                ? "Title verification succeeded."
                : "Title verification failed.");
        }
    }
}
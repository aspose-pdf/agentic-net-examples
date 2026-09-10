using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle = "My Sample PDF";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, set its title, and save it.
        using (Document doc = new Document(inputPath))
        {
            doc.SetTitle(newTitle);          // Set the document title.
            doc.Save(outputPath);            // Save as PDF (no SaveOptions needed for PDF).
        }

        // Re-open the saved PDF to verify the title.
        using (Document verifyDoc = new Document(outputPath))
        {
            string savedTitle = verifyDoc.Info.Title;   // Retrieve the title from DocumentInfo.
            Console.WriteLine($"Saved title: '{savedTitle}'");

            if (savedTitle == newTitle)
                Console.WriteLine("Title verification succeeded.");
            else
                Console.WriteLine("Title verification failed.");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle   = "Accessible Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, set the title, and save it
        using (Document doc = new Document(inputPath))
        {
            // Set the document title
            doc.Info.Title = newTitle;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Re-open the saved PDF to verify the title
        using (Document verifyDoc = new Document(outputPath))
        {
            string savedTitle = verifyDoc.Info.Title ?? "(no title)";
            Console.WriteLine($"Saved PDF title: \"{savedTitle}\"");

            if (savedTitle == newTitle)
                Console.WriteLine("Title verification succeeded.");
            else
                Console.WriteLine("Title verification failed.");
        }
    }
}
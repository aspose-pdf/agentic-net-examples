using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle   = "My Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, set its title, and save it
        using (Document doc = new Document(inputPath))
        {
            // Set the document title using the SetTitle method
            doc.SetTitle(newTitle);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Re-open the saved PDF to verify the title
        using (Document verifyDoc = new Document(outputPath))
        {
            string savedTitle = verifyDoc.Info.Title;

            Console.WriteLine($"Title after saving: \"{savedTitle}\"");
            Console.WriteLine(savedTitle == newTitle
                ? "Title verification succeeded."
                : "Title verification failed.");
        }
    }
}
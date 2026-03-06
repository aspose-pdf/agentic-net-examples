using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // (Optional) Perform any modifications here, e.g. add a blank page:
            // doc.Pages.Add();

            // Save the document as PDF. No SaveOptions are needed because the
            // target format is PDF, which is the default.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document successfully saved to '{outputPath}'.");
    }
}
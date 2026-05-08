using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Set creation and modification dates to the current timestamp
            DateTime now = DateTime.Now; // or DateTime.UtcNow for UTC
            doc.Info.CreationDate = now;
            doc.Info.ModDate      = now;

            // Save the updated PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated timestamps to '{outputPath}'.");
    }
}
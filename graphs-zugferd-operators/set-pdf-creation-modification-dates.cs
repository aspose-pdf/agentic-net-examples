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

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Set creation and modification timestamps to the current time
            DateTime now = DateTime.Now;
            doc.Info.CreationDate = now; // Document creation date
            doc.Info.ModDate      = now; // Document modification date

            // Save the updated PDF (lifecycle rule: save within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF dates updated and saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Set creation and modification dates (metadata)
            doc.Info.CreationDate = DateTime.Now;          // Creation date
            doc.Info.ModDate      = DateTime.Now.AddHours(1); // Example modification date

            // Set custom keywords (metadata)
            doc.Info.Keywords = "Aspose.Pdf;DocumentProperties;Example";

            // Save the updated PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with updated properties to '{outputPath}'.");
    }
}
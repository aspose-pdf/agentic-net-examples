using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Set creation and modification dates to the current processing time
            DateTime now = DateTime.Now;
            doc.Info.CreationDate = now;
            doc.Info.ModDate = now;

            // Add a blank page so the PDF is not empty
            doc.Pages.Add();

            // Save the PDF (PDF format is the default when no SaveOptions are provided)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with timestamps to '{outputPath}'.");
    }
}
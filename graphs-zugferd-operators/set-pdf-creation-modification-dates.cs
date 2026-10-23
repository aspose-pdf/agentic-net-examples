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
            // Add a blank page so the PDF is not empty (optional)
            doc.Pages.Add();

            // Set creation and modification dates to the current processing timestamp
            DateTime now = DateTime.Now;
            doc.Info.CreationDate = now;
            // The correct property for modification date is ModDate (not ModificationDate)
            doc.Info.ModDate = now;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with timestamps to '{outputPath}'.");
    }
}
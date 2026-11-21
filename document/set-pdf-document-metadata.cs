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
            // Add a blank page (required for a valid PDF)
            doc.Pages.Add();

            // Set creation and modification dates
            doc.Info.CreationDate = DateTime.Now;
            doc.Info.ModDate = DateTime.Now;

            // Set custom keywords (comma‑separated string)
            doc.Info.Keywords = "sample, accessibility, Aspose.Pdf";

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with updated properties.");
    }
}
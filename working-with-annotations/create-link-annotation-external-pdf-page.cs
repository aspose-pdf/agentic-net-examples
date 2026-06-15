using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF to modify
        const string outputPath = "output.pdf";        // Resulting PDF
        const string externalPdfPath = "external.pdf"; // Target PDF file
        const int externalPageNumber = 3;              // Page to open in target PDF (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will appear (first page here)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue, // Visual border color
                Contents = $"Open page {externalPageNumber} of external PDF"
            };

            // Assign a remote go‑to action that opens the specified page of the external PDF
            link.Action = new GoToRemoteAction(externalPdfPath, externalPageNumber);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
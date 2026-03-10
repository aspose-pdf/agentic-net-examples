using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a simple text annotation on the first page
            Page page = doc.Pages[1]; // 1‑based page indexing

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Added by Aspose.Pdf",
                Open = true,
                Color = Aspose.Pdf.Color.Yellow
            };

            page.Annotations.Add(txtAnn);

            // Save the modified document back to a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
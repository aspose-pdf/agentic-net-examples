using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Add an extra text annotation on the first page
            Page page = doc.Pages[1];

            // Fully qualified rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Extra Note",
                Contents = "This annotation was added programmatically.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(txtAnn);

            // Save the modified document.
            // Note: Aspose.Pdf does not support saving to PostScript (PS) format.
            // The document is saved as PDF, which is the only supported output format here.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with extra annotation saved to '{outputPath}'.");
    }
}
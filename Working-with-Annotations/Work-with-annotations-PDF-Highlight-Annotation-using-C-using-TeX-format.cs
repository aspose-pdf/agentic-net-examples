using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Annotations;        // HighlightAnnotation

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTex = "output.tex";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle (llx, lly, urx, ury) where the highlight will appear
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], highlightRect);

            // Set visual properties
            highlight.Color    = Aspose.Pdf.Color.Yellow;   // Use Aspose.Pdf.Color (cross‑platform)
            highlight.Contents = "Important text";

            // Attach the annotation to the page
            doc.Pages[1].Annotations.Add(highlight);

            // Save the document to TeX format – must pass TeXSaveOptions explicitly
            TeXSaveOptions texOptions = new TeXSaveOptions();
            doc.Save(outputTex, texOptions);
        }

        Console.WriteLine($"Highlight annotation added and PDF saved as TeX to '{outputTex}'.");
    }
}
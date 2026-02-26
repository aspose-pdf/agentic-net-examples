using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputCgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"File not found: {inputCgmPath}");
            return;
        }

        // Load the CGM file (CGM is input‑only, no CGM save options exist)
        using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation on the specified page and rectangle
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is a text annotation added to a PDF generated from CGM.",
                Open     = true,
                Icon     = TextIcon.Note   // Use the built‑in note icon
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with text annotation saved to '{outputPdfPath}'.");
    }
}
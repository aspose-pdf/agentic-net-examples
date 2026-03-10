using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Output PDF with the added sticky annotation (optional, for verification).
        string outputPdfPath = Path.Combine(dataDir, "output_with_annotations.pdf");

        // Output TeX file generated from the PDF.
        string texPath = Path.Combine(dataDir, "output.tex");

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Select the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = pdfDoc.Pages[1];

            // Define the rectangle where the sticky note will appear.
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (sticky note) and set its properties.
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title    = "Note",                                            // Title shown in the popup title bar.
                Contents = "This is a sticky annotation added via Aspose.Pdf.", // Text displayed in the popup.
                Icon     = TextIcon.Note,                                     // Standard note icon.
                Color    = Aspose.Pdf.Color.Yellow,                           // Background color of the annotation.
                Open     = true                                               // Open the popup by default.
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(sticky);

            // Optional: save the modified PDF so you can inspect the annotation.
            pdfDoc.Save(outputPdfPath);

            // Export the PDF (with the annotation) to TeX format.
            // TeXSaveOptions is required because Save(string) without options always writes PDF.
            TeXSaveOptions texOptions = new TeXSaveOptions();
            pdfDoc.Save(texPath, texOptions);
        }

        Console.WriteLine("Sticky annotation added and PDF exported to TeX successfully.");
    }
}
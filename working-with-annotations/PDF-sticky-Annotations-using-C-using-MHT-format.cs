using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Directory containing the source MHT file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input MHT file path.
        string mhtFile = Path.Combine(dataDir, "input.mht");

        // Output PDF file path (PDF is the only supported export format).
        string outputPdf = Path.Combine(dataDir, "output.pdf");

        // Verify that the MHT file exists.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Load options for MHT import.
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Load the MHT file into a PDF document.
        using (Document pdfDoc = new Document(mhtFile, loadOptions))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = pdfDoc.Pages[1];

            // Define the rectangle where the sticky note will appear.
            // Fully qualified type avoids ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a TextAnnotation (sticky note) on the specified page.
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title    = "Note", // Title shown in the annotation's popup window.
                Contents = "This is a sticky annotation added to an MHT‑converted PDF.",
                Color    = Aspose.Pdf.Color.Yellow, // Background color of the annotation.
                Open     = true,                    // Show the popup open by default.
                Icon     = TextIcon.Comment          // Standard comment icon.
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(sticky);

            // Save the modified document as PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sticky annotation saved to '{outputPdf}'.");
    }
}
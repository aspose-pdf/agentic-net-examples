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

        // Open the source PDF (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the sticky‑note icon will appear
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a TextAnnotation (the visible sticky note)
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",                     // Title shown in the pop‑up window title bar
                Contents = "Hover to see details",     // Short tooltip text
                // Icon defaults to Note; can be changed via textAnn.Icon if desired
                Open     = false                       // Start closed; will open on hover/click
            };

            // Create a PopupAnnotation that holds the detailed information
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                Contents = "This is the additional information displayed in the pop‑up window.",
                Open     = false                       // Initially closed
            };

            // Link the popup to the text annotation
            textAnn.Popup = popup;

            // Add the annotation (and its linked popup) to the page
            page.Annotations.Add(textAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pop‑up note annotation added and saved to '{outputPath}'.");
    }
}
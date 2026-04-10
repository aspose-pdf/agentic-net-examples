using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Rectangle for the visible sticky‑note icon
            Aspose.Pdf.Rectangle noteRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (the sticky‑note icon)
            TextAnnotation textAnn = new TextAnnotation(page, noteRect)
            {
                Title    = "Info",                     // Title shown in the popup window title bar
                Contents = "Hover for more details.", // Short tooltip text
                Icon     = TextIcon.Note,              // Standard note icon
                Open     = false                       // Do not display the popup open by default
            };

            // Rectangle for the popup window (size and position)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(250, 600, 450, 800);

            // Create the PopupAnnotation that holds the detailed information
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is the detailed information that appears when the user hovers over the note annotation.",
                Open     = false // Popup remains hidden until the user activates the note
            };

            // Link the popup to its parent note annotation
            popup.Parent = textAnn;
            textAnn.Popup = popup; // optional; establishes the same relationship

            // Add both annotations to the page
            page.Annotations.Add(textAnn);
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with popup note: {outputPath}");
    }
}
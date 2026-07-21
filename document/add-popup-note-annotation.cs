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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the visible sticky‑note icon
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a TextAnnotation (the sticky‑note icon)
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",                     // Title shown in the popup window title bar
                Contents = "Brief note shown on the page.", // Text shown when the icon is clicked
                Icon     = TextIcon.Comment,           // Icon type (Comment, Note, etc.)
                Open     = false                       // Do not open automatically
            };

            // Create a PopupAnnotation that holds the detailed information
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                Contents = "This is the detailed information displayed when the user hovers over the note.",
                Open     = false                       // Initially closed; shown on hover/click
            };

            // Associate the popup with its parent text annotation
            popup.Parent = textAnn;
            textAnn.Popup = popup;

            // Add the text annotation (which now references the popup) to the page
            page.Annotations.Add(textAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with pop‑up note saved to '{outputPath}'.");
    }
}
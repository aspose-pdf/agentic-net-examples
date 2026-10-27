using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // required by task specification

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "popup_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 800);

            // Create a TextAnnotation that will hold the multi‑line comment
            TextAnnotation textAnnot = new TextAnnotation(page, rect)
            {
                // Use bullet characters (U+2022) and CRLF for line breaks
                Contents = "• First comment\r\n• Second comment\r\n• Third comment",
                Title    = "Detailed Comments",
                Open     = false,                     // popup closed by default
                Icon     = TextIcon.Comment           // choose an appropriate icon
            };

            // Add the TextAnnotation to the page
            page.Annotations.Add(textAnnot);

            // Create a PopupAnnotation linked to the TextAnnotation
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                // Associate the popup with its parent annotation
                Parent = textAnnot,
                // Optionally enlarge the popup rectangle
                Rect   = new Aspose.Pdf.Rectangle(120, 720, 380, 880),
                // The popup displays the same contents as the TextAnnotation
                Contents = textAnnot.Contents
            };

            // Add the PopupAnnotation to the page
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added. Saved to '{outputPath}'.");
    }
}
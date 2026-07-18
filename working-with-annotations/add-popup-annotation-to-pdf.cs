using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF or create a new one
        const string outputPath = "output_with_popup.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPath);
            }
        }

        // Open the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the parent markup annotation (a sticky note)
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (sticky note) on the page
            TextAnnotation parentAnnotation = new TextAnnotation(page, parentRect)
            {
                Icon     = TextIcon.Note,               // visual icon
                Color    = Aspose.Pdf.Color.Yellow,     // border color
                Title    = "Reviewer",                  // title shown in the popup window title bar
                Contents = "Brief comment",             // short text shown when the note is collapsed
                Open     = false                        // do not display the popup automatically
            };

            // Define the rectangle for the popup annotation (larger area for detailed notes)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(210, 500, 410, 650);

            // Create the PopupAnnotation and associate it with the parent markup annotation
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                // Detailed note that appears when the parent annotation is selected
                Contents = "This is an extended note providing additional context and information about the comment.",
                // Optional: you can set a plain text subject or title if needed
                // Subject = "Extended Note",
                Open     = false, // initially closed; will open when parent is selected
                Parent   = parentAnnotation // link to the parent markup annotation
            };

            // Add both annotations to the page's annotation collection
            page.Annotations.Add(parentAnnotation);
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}

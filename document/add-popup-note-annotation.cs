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

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a TextAnnotation (the sticky‑note icon that the user sees)
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Icon     = TextIcon.Comment,          // icon displayed on the page
                Title    = "Quick Note",              // title shown in the popup window
                Contents = "Hover to see details.",   // short tooltip text
                Open     = false                      // popup is closed by default
            };

            // Create a PopupAnnotation that holds the detailed information
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                Contents = "This is the detailed information that appears when the user hovers over the note annotation.",
                Open     = false                      // keep it closed until the user activates it
            };

            // Associate the popup with its parent text annotation
            popup.Parent = textAnn;

            // Add both annotations to the page's annotation collection
            page.Annotations.Add(textAnn);
            page.Annotations.Add(popup);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with pop‑up note saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Rectangle for the parent text annotation (where the user hovers)
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            TextAnnotation parent = new TextAnnotation(page, parentRect)
            {
                Title = "Info",
                Contents = "Hover to see note",
                Open = false // not opened by default
            };

            // Rectangle for the popup window itself
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(250, 500, 450, 650);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is a note that appears when you hover over the annotation.",
                Open = false // show on hover, not initially open
            };

            // Associate the popup with the parent annotation
            parent.Popup = popup;

            // Add the parent annotation (which carries the popup) to the page
            page.Annotations.Add(parent);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}

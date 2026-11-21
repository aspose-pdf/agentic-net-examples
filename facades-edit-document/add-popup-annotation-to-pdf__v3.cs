using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);
        // Get the first page (pages are 1‑based)
        Page page = doc.Pages[1];

        // Rectangle for the trigger annotation (the icon the user hovers over)
        var triggerRect = new Rectangle(100, 500, 200, 550);

        // Create a TextAnnotation that will act as the trigger for the popup
        var textAnn = new TextAnnotation(page, triggerRect)
        {
            Title = "Hover Note",
            Contents = "This note appears when the user hovers over the annotation.",
            Icon = TextIcon.Note,
            Open = false // do not open automatically; the popup will appear on hover
        };
        page.Annotations.Add(textAnn);

        // Rectangle for the actual popup window (can be larger than the trigger)
        var popupRect = new Rectangle(210, 500, 400, 650);

        // Create the PopupAnnotation and link it to the TextAnnotation
        var popupAnn = new PopupAnnotation(page, popupRect)
        {
            Parent = textAnn // associate the popup with its trigger annotation
        };
        page.Annotations.Add(popupAnn);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Popup annotation added. Saved to '{outputPath}'.");
    }
}

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the parent markup annotation (a sticky note)
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a TextAnnotation (sticky note) as the parent markup annotation
            TextAnnotation textAnn = new TextAnnotation(page, parentRect)
            {
                Title    = "Note",
                Contents = "Click to see more details.",
                Color    = Aspose.Pdf.Color.Yellow,
                Icon     = TextIcon.Note,
                Open     = false   // Do not open automatically
            };

            // Define the rectangle for the popup annotation (size of the popup window)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(130, 720, 300, 850);

            // Create the PopupAnnotation
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Additional notes go here. This text appears in the popup window when the parent annotation is selected.",
                Open     = false   // Popup is closed initially; it opens when the parent is clicked
            };

            // Associate the popup with its parent markup annotation
            // Option 1: assign via the Popup property of the parent
            textAnn.Popup = popup;

            // Optionally, also set the Parent property of the popup (both ways are safe)
            popup.Parent = textAnn;

            // Add the parent annotation to the page (the popup is linked through the parent)
            page.Annotations.Add(textAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with popup annotation: {outputPath}");
    }
}
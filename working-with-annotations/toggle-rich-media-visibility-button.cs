using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "ButtonToggleRichMedia.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define rectangle for the RichMedia annotation (e.g., a video placeholder)
            Aspose.Pdf.Rectangle richMediaRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            // Create the RichMedia annotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, richMediaRect)
            {
                // Example: set a simple text as placeholder content
                Contents = "Rich Media Content"
            };
            // Add the RichMedia annotation to the page
            page.Annotations.Add(richMedia);

            // Define rectangle for the button that will toggle visibility
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(350, 500, 450, 540);
            // Create a push button field (instantiate first, then set properties)
            ButtonField toggleButton = new ButtonField(page, buttonRect)
            {
                Color = Aspose.Pdf.Color.LightGray,
                NormalCaption = "Toggle Media",
                // A name is required for form fields; give it a unique identifier
                Name = "ToggleRichMediaBtn"
            };
            // Border requires the parent annotation, set after the button is instantiated
            toggleButton.Border = new Border(toggleButton) { Width = 1 };

            // When the button is pressed, hide the RichMedia annotation
            toggleButton.Actions.OnPressMouseBtn = new HideAction(richMedia, true);
            // When the button is released, show the RichMedia annotation again
            toggleButton.Actions.OnReleaseMouseBtn = new HideAction(richMedia, false);

            // Add the button to the document's form collection (widgets cannot be added directly to page annotations)
            doc.Form.Add(toggleButton);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

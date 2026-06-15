using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF file
        using (Document createDoc = new Document())
        {
            Page createPage = createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Step 2: Open the sample PDF and add annotations
        using (Document doc = new Document("input.pdf"))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define rectangle for the RichMedia annotation
            Aspose.Pdf.Rectangle richMediaRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            // Create RichMedia annotation (no actual media content for simplicity)
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, richMediaRect);
            richMedia.Name = "MyRichMedia";
            richMedia.Type = RichMediaAnnotation.ContentType.Video;
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.Click;
            // Add the RichMedia annotation to the page
            page.Annotations.Add(richMedia);

            // Define rectangle for the button (using a Link annotation as a clickable button)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(350, 500, 450, 550);
            LinkAnnotation toggleButton = new LinkAnnotation(page, buttonRect);
            toggleButton.Contents = "Toggle Media";
            // Create a HideAction that hides the RichMedia annotation when the button is clicked
            HideAction hideAction = new HideAction(richMedia, true);
            // Add the action to the button's action collection
            toggleButton.Actions.Add(hideAction);
            // Add the button annotation to the page
            page.Annotations.Add(toggleButton);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}

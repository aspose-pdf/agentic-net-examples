using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a RichMediaAnnotation
            Aspose.Pdf.Rectangle richRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, richRect);
            richMedia.Contents = "Rich media content placeholder";
            page.Annotations.Add(richMedia);

            // Create a button that will toggle the RichMediaAnnotation visibility
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(350, 500, 450, 550);
            ButtonField toggleButton = new ButtonField(page, btnRect);
            toggleButton.Contents = "Toggle Media";
            toggleButton.Color = Aspose.Pdf.Color.LightGray;

            // HideAction to hide the RichMediaAnnotation
            HideAction hideAction = new HideAction(richMedia, true);
            // HideAction to show the RichMediaAnnotation
            HideAction showAction = new HideAction(richMedia, false);

            // Assign actions: press hides, release shows (toggles visibility)
            toggleButton.Actions.OnPressMouseBtn = hideAction;
            toggleButton.Actions.OnReleaseMouseBtn = showAction;

            // Add the button to the page
            page.Annotations.Add(toggleButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
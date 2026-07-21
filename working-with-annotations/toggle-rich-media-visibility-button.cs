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
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // -----------------------------------------------------------------
            // 1. Create a RichMediaAnnotation (placeholder – no actual media file)
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle richRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, richRect)
            {
                Name = "myRichMedia",
                // Ensure the annotation is initially visible (no Hidden flag)
                Flags = AnnotationFlags.Print
            };
            page.Annotations.Add(richMedia);

            // -----------------------------------------------------------------
            // 2. Create a push button that will toggle the visibility of the
            //    RichMediaAnnotation when the user clicks it.
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(350, 500, 450, 540);
            ButtonField toggleBtn = new ButtonField(doc, btnRect)
            {
                Name = "toggleButton",
                AlternateCaption = "Toggle Media",
                Highlighting = HighlightingMode.Push
            };

            // HideAction can hide or show an annotation. Setting the second
            // parameter to true means the action will hide the target annotation.
            // When the button is pressed, this action will be executed.
            HideAction hideAction = new HideAction(richMedia, true);

            // Assign the hide action to the button's mouse‑press event.
            toggleBtn.Actions.OnPressMouseBtn = hideAction;

            // Add the button to the page.
            page.Annotations.Add(toggleBtn);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
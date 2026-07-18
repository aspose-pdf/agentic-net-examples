using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "button_floatingbox.pdf";

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Create a push‑button form field (AcroForm) – use ButtonField (PushButtonField does not exist)
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            var button = new ButtonField(page, buttonRect)
            {
                PartialName = "hoverButton",
                Value = "Hover me"
            };
            // Register the button with the document's form collection
            doc.Form.Add(button, 1);

            // -----------------------------------------------------------------
            // Create a floating box represented by a SquareAnnotation
            // (FloatingBox is a paragraph element; using an annotation allows HideAction)
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle boxRect = new Aspose.Pdf.Rectangle(250, 600, 450, 750);
            var floatingBox = new SquareAnnotation(page, boxRect)
            {
                Color = Aspose.Pdf.Color.LightGray // background colour
            };
            // Border requires the parent annotation in its constructor
            floatingBox.Border = new Border(floatingBox) { Width = 1 };
            // Hide the annotation initially using the Flags property (Hidden flag)
            floatingBox.Flags = AnnotationFlags.Hidden;
            page.Annotations.Add(floatingBox);

            // -----------------------------------------------------------------
            // Assign a HideAction to the button's mouse‑enter/exit events to show/hide the box
            // HideAction(annotation, isHidden) – set isHidden to false to display
            // -----------------------------------------------------------------
            button.Actions.OnEnter = new HideAction(floatingBox, false);
            button.Actions.OnExit  = new HideAction(floatingBox, true);

            // Save the PDF (Document.Save without SaveOptions writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}

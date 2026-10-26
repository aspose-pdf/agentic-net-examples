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
        const string outputPath = "output_with_toggle_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the button will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a button form field (the PDF annotation that behaves like a button)
            ButtonField btn = new ButtonField(page, btnRect)
            {
                // The name used in the form hierarchy (must be unique within the document)
                PartialName = "ToggleAnnotationsButton",
                // Text displayed on the button (use NormalCaption for visual label)
                NormalCaption = "Toggle Annotations"
            };

            // Set visual border for the button (Border requires the parent annotation in the ctor)
            btn.Border = new Border(btn) { Width = 1 };
            // Set border colour – use the Color property (BorderColor does not exist)
            btn.Color = Color.Black;

            // JavaScript that toggles the visibility of all annotations on the current page
            string js = @"
var annots = this.getAnnots();
if (annots != null) {
    for (var i = 0; i < annots.length; i++) {
        annots[i].hidden = !annots[i].hidden;
    }
}";

            // Assign the JavaScript to the button's mouse‑press action (OnPressMouseBtn is the valid property)
            btn.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form (or directly to the page annotations)
            doc.Form.Add(btn, 1);
            // Alternatively: page.Annotations.Add(btn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'");
    }
}

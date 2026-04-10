using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toggle_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a push button field on the page
            ButtonField toggleButton = new ButtonField(page, btnRect)
            {
                Name = "ToggleAnnotationsButton",
                NormalCaption = "Toggle Annotations",
                Color = Aspose.Pdf.Color.LightGray,
                Highlighting = HighlightingMode.Push
            };

            // JavaScript that toggles the hidden flag of every annotation on the current page
            string jsCode = @"
var annots = this.getAnnots();
if (annots != null) {
    for (var i = 0; i < annots.length; i++) {
        annots[i].hidden = !annots[i].hidden;
    }
}
";

            // Assign the JavaScript to the button's mouse‑press action
            toggleButton.Actions.OnPressMouseBtn = new JavascriptAction(jsCode);

            // Add the button to the page's annotation collection
            page.Annotations.Add(toggleButton);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'");
    }
}
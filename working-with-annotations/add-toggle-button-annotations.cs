using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (change index as needed)
            Page page = doc.Pages[1];

            // Define button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a push button field on the page
            ButtonField toggleButton = new ButtonField(page, btnRect)
            {
                Name = "ToggleAnnotations",
                NormalCaption = "Toggle Annotations"
            };

            // JavaScript to toggle visibility of all annotations on the current page
            string jsCode = @"
                var ann = this.getAnnots();
                if (ann != null) {
                    for (var i = 0; i < ann.length; i++) {
                        ann[i].hidden = !ann[i].hidden;
                    }
                }
            ";

            // Attach the JavaScript to the button's mouse‑press action
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            toggleButton.Actions.OnPressMouseBtn = jsAction;

            // Add the button to the page annotations collection
            page.Annotations.Add(toggleButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'");
    }
}
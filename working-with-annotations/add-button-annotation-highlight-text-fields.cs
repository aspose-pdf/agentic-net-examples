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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the button will be placed (first page in this example)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the button's rectangular area (llx, lly, urx, ury)
            Rectangle buttonRect = new Rectangle(100, 700, 200, 750);

            // Create a form button field
            ButtonField button = new ButtonField(page, buttonRect)
            {
                PartialName = "HighlightBtn",
                // Use AlternateCaption (or NormalCaption) instead of the non‑existent Caption property
                AlternateCaption = "Highlight Text Fields"
            };

            // Set a simple border for visual clarity
            button.Border = new Border(button) { Width = 1 };

            // JavaScript that highlights all text fields on the current page
            string js = @"
var annots = this.getAnnots();
if (annots != null) {
    for (var i = 0; i < annots.length; i++) {
        var a = annots[i];
        if (a.type == 'Widget' && a.fieldType == 'Tx') {
            a.fillColor = color.yellow;
        }
    }
}";

            // Assign the JavaScript action to the button's mouse‑press event
            button.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form collection (not to page.Annotations)
            doc.Form.Add(button, page.Number);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with button annotation saved to '{outputPath}'.");
    }
}

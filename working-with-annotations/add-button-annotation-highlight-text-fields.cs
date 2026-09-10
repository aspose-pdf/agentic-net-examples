using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Choose the page where the button will be placed (first page in this example)
                Page page = doc.Pages[1];

                // Define the button rectangle (coordinates are in points, lower‑left origin)
                Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

                // Create a push button field on the selected page
                ButtonField highlightBtn = new ButtonField(page, btnRect)
                {
                    PartialName      = "HighlightBtn",
                    AlternateCaption = "Highlight Text Fields",
                    Color            = Aspose.Pdf.Color.LightGray
                };

                // JavaScript that iterates over all fields on the current page
                // and sets a yellow background for each text field.
                string jsCode = @"
var i;
for (i = 0; i < this.numFields; i++) {
    var f = this.getField(this.getFieldName(i));
    if (f.type == 'text') {
        f.fillColor = color.yellow;
    }
}";

                // Assign the JavaScript to the button's mouse‑press action
                highlightBtn.Actions.OnPressMouseBtn = new JavascriptAction(jsCode);

                // Add the button annotation to the page
                page.Annotations.Add(highlightBtn);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with button annotation: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
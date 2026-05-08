using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Annotations; // for JavascriptAction

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the button will be placed (first page in this example)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the button rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create the button field on the selected page
            ButtonField button = new ButtonField(page, btnRect)
            {
                Name = "HighlightTextFieldsBtn",
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray,
                Highlighting = HighlightingMode.Push,
                Contents = "Highlight Text Fields"
            };

            // JavaScript to highlight all text fields on the current page
            string js = @"
var fields = this.getFieldNames();
for (var i = 0; i < fields.length; i++) {
    var f = this.getField(fields[i]);
    if (f.type == 'text' && f.page == this.pageNum) {
        f.fillColor = color.yellow;
    }
}";
            // Assign the JavaScript to the button's activation action
            button.OnActivated = new JavascriptAction(js);

            // Add the button to the page annotations collection
            page.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: '{outputPath}'.");
    }
}
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

        // JavaScript that highlights all text fields on the current page
        string js = @"
var pageNum = this.pageNum;
var annots = this.getAnnots(pageNum);
if (annots != null) {
    for (var i = 0; i < annots.length; i++) {
        var a = annots[i];
        if (a.type == 'Widget' && a.fieldType == 'Tx') {
            a.fillColor = color.yellow;
        }
    }
}
";

        using (Document doc = new Document(inputPath))
        {
            // Define button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a button form field on the first page
            ButtonField button = new ButtonField(doc.Pages[1], rect)
            {
                PartialName = "HighlightAllTextFields",
                Value = "Highlight"
            };

            // Assign JavaScript to be executed when the button is pressed
            button.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form (AcroForm)
            doc.Form.Add(button, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation to '{outputPath}'.");
    }
}

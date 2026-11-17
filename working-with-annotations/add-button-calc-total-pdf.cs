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
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the button's position using Aspose.Pdf.Rectangle (avoid System.Drawing)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a button field (interactive button) on the page
            ButtonField button = new ButtonField(page, rect);
            button.PartialName = "CalculateTotal"; // internal name of the field
            button.Value = "Calculate Total";      // caption shown on the button

            // JavaScript that sums numeric fields "field1" and "field2" and writes to "total"
            string js = @"
var f1 = this.getField('field1').value;
var f2 = this.getField('field2').value;
var total = parseFloat(f1) + parseFloat(f2);
this.getField('total').value = total;
";
            // Assign the JavaScript to the PressMouseBtn action (executed when the button is clicked)
            button.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button field to the document's form collection (page 1)
            doc.Form.Add(button, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation to '{outputPath}'.");
    }
}

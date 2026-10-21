using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Define the rectangle for the button (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        var rect = new Rectangle(100, 500, 200, 540);

        // Create a push‑button field (Aspose.Pdf uses ButtonField for push buttons)
        var calcButton = new ButtonField(doc.Pages[1], rect)
        {
            PartialName = "CalcButton",
            // The caption of a button is set via the Value property
            Value = "Calculate Total"
        };

        // JavaScript that reads "Quantity" and "UnitPrice" fields, multiplies them, and writes to "Total"
        string js = @"var qty   = this.getField('Quantity').value;
var price = this.getField('UnitPrice').value;
var total = parseFloat(qty) * parseFloat(price);
this.getField('Total').value = total;";

        // Attach the JavaScript to the button – use the OnPressMouseBtn action for a button field
        calcButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

        // Add the button to the form on page 1
        doc.Form.Add(calcButton, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF with JavaScript button saved to '{outputPath}'.");
    }
}

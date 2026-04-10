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
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 530);

            // Create a push button field on the page
            ButtonField calcButton = new ButtonField(page, buttonRect)
            {
                PartialName = "CalcButton",
                NormalCaption = "Calculate"
            };

            // JavaScript that reads Quantity and UnitPrice fields,
            // computes the total, and writes it to the Total field
            string js = @"
var qty = this.getField('Quantity').value;
var price = this.getField('UnitPrice').value;
var total = qty * price;
this.getField('Total').value = total;
";

            // Attach the script to the button's mouse‑press action
            calcButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the form on page 1
            doc.Form.Add(calcButton, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with calculation button: {outputPath}");
    }
}
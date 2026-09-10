using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF that already contains Quantity, UnitPrice and Total fields
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field on the document
            ButtonField calcButton = new ButtonField(doc, btnRect)
            {
                // Set a name for the button (used as the field identifier)
                PartialName = "CalcButton",
                // Caption shown on the button
                NormalCaption = "Calculate Total"
            };

            // JavaScript that reads Quantity and UnitPrice fields, computes Total, and writes it back
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                var total = qty * price;
                this.getField('Total').value = total;
            ";

            // Attach the JavaScript to the button's mouse‑up (release) action – the correct property is OnReleaseMouseBtn
            calcButton.Actions.OnReleaseMouseBtn = new JavascriptAction(js);

            // Add the button to the form (page number is optional because the button already knows its page)
            doc.Form.Add(calcButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculation button saved to '{outputPath}'.");
    }
}

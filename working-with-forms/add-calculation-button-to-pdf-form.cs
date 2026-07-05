using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF that already contains Quantity, UnitPrice and Total fields
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Define the button rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 530);

            // Create a push button field on the document
            ButtonField calcButton = new ButtonField(doc, btnRect)
            {
                PartialName   = "CalcButton",          // internal field name
                NormalCaption = "Calculate Total"      // text shown on the button
            };

            // JavaScript that reads Quantity and UnitPrice fields, computes Total, and writes it back
            string jsCode = @"
                var qty   = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                var total = qty * price;
                this.getField('Total').value = total;
            ";

            // Assign the JavaScript to the button's mouse‑press action (valid property for AnnotationActionCollection)
            calcButton.Actions.OnPressMouseBtn = new JavascriptAction(jsCode);

            // Add the button to the form (page placement is already defined by the rectangle)
            doc.Form.Add(calcButton);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with calculation button saved to '{outputPdf}'.");
    }
}

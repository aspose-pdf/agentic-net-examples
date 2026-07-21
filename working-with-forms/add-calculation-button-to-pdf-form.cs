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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure form fields are recalculated automatically when a field changes
            doc.Form.AutoRecalculate = true;

            // Quantity field
            Aspose.Pdf.Rectangle qtyRect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            TextBoxField qtyField = new TextBoxField(doc, qtyRect)
            {
                PartialName = "Quantity",
                Contents    = "0"
            };
            doc.Form.Add(qtyField);

            // Unit price field
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(100, 660, 200, 680);
            TextBoxField priceField = new TextBoxField(doc, priceRect)
            {
                PartialName = "UnitPrice",
                Contents    = "0"
            };
            doc.Form.Add(priceField);

            // Total field (read‑only)
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 620, 200, 640);
            TextBoxField totalField = new TextBoxField(doc, totalRect)
            {
                PartialName = "Total",
                ReadOnly    = true,
                Contents    = "0"
            };
            doc.Form.Add(totalField);

            // Button that triggers the calculation
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 580, 200, 600);
            ButtonField calcButton = new ButtonField(doc, btnRect)
            {
                PartialName = "CalcButton",
                NormalCaption = "Calculate"
            };

            // Attach JavaScript to the button's activation action
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                var total = parseFloat(qty) * parseFloat(price);
                this.getField('Total').value = total;
            ";
            calcButton.OnActivated = new JavascriptAction(js);

            doc.Form.Add(calcButton);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculation button saved to '{outputPath}'.");
    }
}
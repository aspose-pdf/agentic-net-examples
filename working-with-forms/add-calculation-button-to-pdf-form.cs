using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "price_calculator.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Quantity text field
            TextBoxField qtyField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 200, 720));
            qtyField.PartialName = "Quantity";
            qtyField.Value = "0";
            doc.Form.Add(qtyField);

            // Unit Price text field
            TextBoxField priceField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 650, 200, 670));
            priceField.PartialName = "UnitPrice";
            priceField.Value = "0.00";
            doc.Form.Add(priceField);

            // Total text field (read‑only)
            TextBoxField totalField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 200, 620));
            totalField.PartialName = "Total";
            totalField.Value = "0.00";
            totalField.ReadOnly = true;
            doc.Form.Add(totalField);

            // Button that runs JavaScript to calculate total = Quantity * UnitPrice
            ButtonField calcButton = new ButtonField(page, new Aspose.Pdf.Rectangle(100, 550, 200, 570));
            calcButton.PartialName = "CalcButton";
            calcButton.Value = "Calculate";

            string js = @"
var qty = this.getField('Quantity').value;
var price = this.getField('UnitPrice').value;
var total = parseFloat(qty) * parseFloat(price);
this.getField('Total').value = total.toFixed(2);
";
            // Assign JavaScript to the button's mouse‑press action (click)
            calcButton.Actions.OnPressMouseBtn = new JavascriptAction(js);
            doc.Form.Add(calcButton);

            // Add visible labels for the fields
            TextFragment lblQty = new TextFragment("Quantity:");
            lblQty.Position = new Position(20, 710);
            page.Paragraphs.Add(lblQty);

            TextFragment lblPrice = new TextFragment("Unit Price:");
            lblPrice.Position = new Position(20, 660);
            page.Paragraphs.Add(lblPrice);

            TextFragment lblTotal = new TextFragment("Total:");
            lblTotal.Position = new Position(20, 610);
            page.Paragraphs.Add(lblTotal);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

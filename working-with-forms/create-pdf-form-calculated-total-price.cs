using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, right, top)
            var qtyRect   = new Aspose.Pdf.Rectangle(100, 700, 200, 730);
            var priceRect = new Aspose.Pdf.Rectangle(100, 650, 200, 680);
            var totalRect = new Aspose.Pdf.Rectangle(100, 600, 200, 630);

            // Quantity field (NumberField)
            var qtyField = new NumberField(page, qtyRect)
            {
                PartialName = "Quantity",
                Value = "0",               // default value
                Color = Aspose.Pdf.Color.LightGray
            };
            doc.Form.Add(qtyField);

            // Unit Price field (NumberField)
            var priceField = new NumberField(page, priceRect)
            {
                PartialName = "UnitPrice",
                Value = "0",              // default value
                Color = Aspose.Pdf.Color.LightGray
            };
            doc.Form.Add(priceField);

            // Total Price field (NumberField) – calculated
            var totalField = new NumberField(page, totalRect)
            {
                PartialName = "TotalPrice",
                ReadOnly = true,          // user should not edit directly
                Color = Aspose.Pdf.Color.LightGray
            };

            // JavaScript to calculate total = quantity * unit price
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                // Ensure numeric conversion
                qty = parseFloat(qty);
                price = parseFloat(price);
                if (isNaN(qty)) qty = 0;
                if (isNaN(price)) price = 0;
                event.value = (qty * price).toFixed(2);
            ";
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            doc.Form.Add(totalField);

            // Enable automatic recalculation of calculated fields when the document is saved
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated total price saved to '{outputPath}'.");
    }
}
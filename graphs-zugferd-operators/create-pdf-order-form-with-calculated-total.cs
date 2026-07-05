using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define rectangles for the form fields (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(50, 700, 150, 720);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(160, 700, 260, 720);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(270, 700, 370, 720);

            // Create Quantity field
            TextBoxField qtyField = new TextBoxField(page, qtyRect)
            {
                PartialName = "Qty1",
                Value       = "0"
            };

            // Create Unit Price field
            TextBoxField priceField = new TextBoxField(page, priceRect)
            {
                PartialName = "Price1",
                Value       = "0"
            };

            // Create Total field (read‑only)
            TextBoxField totalField = new TextBoxField(page, totalRect)
            {
                PartialName = "Total",
                ReadOnly    = true,
                Value       = "0"
            };

            // Add fields to the form
            doc.Form.Add(qtyField);
            doc.Form.Add(priceField);
            doc.Form.Add(totalField);

            // JavaScript that calculates Total = Qty * Price
            string jsCalc = @"
                var qty   = this.getField('Qty1').value;
                var price = this.getField('Price1').value;
                var total = parseFloat(qty) * parseFloat(price);
                if (!isNaN(total)) {
                    this.getField('Total').value = total.toFixed(2);
                } else {
                    this.getField('Total').value = '';
                }";

            // Attach the calculation script to Quantity and Price fields
            JavascriptAction calcAction = new JavascriptAction(jsCalc);
            qtyField.Actions.OnCalculate   = calcAction;
            priceField.Actions.OnCalculate = calcAction;

            // Ensure the form recalculates fields automatically
            doc.Form.AutoRecalculate = true;

            // Define calculation order (Total must be calculated after Qty and Price)
            doc.Form.CalculatedFields = new Field[] { totalField };

            // Force an initial calculation (optional)
            totalField.Recalculate();

            // Save the PDF
            doc.Save("order_form.pdf");
        }

        Console.WriteLine("PDF with calculated total field created successfully.");
    }
}
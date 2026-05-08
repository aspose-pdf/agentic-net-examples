using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction
using Aspose.Pdf.Forms;      // Form fields

class Program
{
    static void Main()
    {
        const string outputPath = "OrderForm.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, right, top)
            // Note: Aspose.Pdf.Rectangle expects (llx, lly, urx, ury)
            var qtyRect   = new Aspose.Pdf.Rectangle(100f, 700f, 200f, 720f);
            var priceRect = new Aspose.Pdf.Rectangle(100f, 650f, 200f, 670f);
            var totalRect = new Aspose.Pdf.Rectangle(100f, 600f, 200f, 620f);

            // Create Quantity field
            TextBoxField qtyField = new TextBoxField(page, qtyRect)
            {
                PartialName = "Quantity",
                Value = "0"
            };
            // Create Price field
            TextBoxField priceField = new TextBoxField(page, priceRect)
            {
                PartialName = "Price",
                Value = "0"
            };
            // Create Total field (read‑only)
            TextBoxField totalField = new TextBoxField(page, totalRect)
            {
                PartialName = "Total",
                Value = "0",
                ReadOnly = true
            };

            // Add fields to the form on page 1 (Aspose.Pdf uses 1‑based indexing)
            doc.Form.Add(qtyField, 1);
            doc.Form.Add(priceField, 1);
            doc.Form.Add(totalField, 1);

            // JavaScript that calculates Total = Quantity * Price
            string jsCode = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('Price').value;
                qty = parseFloat(qty);
                price = parseFloat(price);
                if (!isNaN(qty) && !isNaN(price)) {
                    event.value = (qty * price).toFixed(2);
                } else {
                    event.value = '';
                }
            ";

            // Attach the script to the Calculate action of the Total field
            totalField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Ensure automatic recalculation (default is true, set explicitly for clarity)
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}

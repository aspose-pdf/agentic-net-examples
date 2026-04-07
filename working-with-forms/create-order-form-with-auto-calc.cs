using System;
using System.Drawing; // needed for System.Drawing.Color in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "OrderForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, width, height)
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(50, 700, 100, 20);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(200, 700, 100, 20);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(350, 700, 100, 20);

            // Create Quantity field
            TextBoxField qtyField = new TextBoxField(page, qtyRect);
            qtyField.PartialName = "Quantity";
            qtyField.Value = "0";
            qtyField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(qtyField, "Quantity", 1);

            // Create Unit Price field
            TextBoxField priceField = new TextBoxField(page, priceRect);
            priceField.PartialName = "UnitPrice";
            priceField.Value = "0.00";
            priceField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(priceField, "UnitPrice", 1);

            // Create Total field (read‑only)
            TextBoxField totalField = new TextBoxField(page, totalRect);
            totalField.PartialName = "Total";
            totalField.Value = "0.00";
            totalField.ReadOnly = true;
            totalField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(totalField, "Total", 1);

            // JavaScript to calculate Total = Quantity * UnitPrice
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                // Ensure numeric calculation
                qty = parseFloat(qty);
                price = parseFloat(price);
                if (!isNaN(qty) && !isNaN(price)) {
                    this.getField('Total').value = (qty * price).toFixed(2);
                } else {
                    this.getField('Total').value = '';
                }
            ";

            // Attach the script to the Total field's calculate action
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // Ensure automatic recalculation (default is true, set explicitly for clarity)
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
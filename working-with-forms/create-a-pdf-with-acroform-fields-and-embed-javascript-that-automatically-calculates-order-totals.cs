using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "OrderForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(50, 750, 150, 770);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(200, 750, 300, 770);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(350, 750, 450, 770);

            // Create Quantity field
            TextBoxField qtyField = new TextBoxField(page, qtyRect);
            qtyField.PartialName = "Quantity";
            qtyField.Value = "0";
            doc.Form.Add(qtyField, 1); // Add to page 1

            // Create Price field
            TextBoxField priceField = new TextBoxField(page, priceRect);
            priceField.PartialName = "Price";
            priceField.Value = "0.00";
            doc.Form.Add(priceField, 1);

            // Create Total field (read‑only, calculated)
            TextBoxField totalField = new TextBoxField(page, totalRect);
            totalField.PartialName = "Total";
            totalField.ReadOnly = true;
            doc.Form.Add(totalField, 1);

            // Enable automatic recalculation for the whole form
            doc.Form.AutoRecalculate = true;

            // JavaScript to calculate Total = Quantity * Price
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('Price').value;
                // Convert to numbers, handle empty strings
                qty = isNaN(qty) ? 0 : parseFloat(qty);
                price = isNaN(price) ? 0 : parseFloat(price);
                this.getField('Total').value = (qty * price).toFixed(2);
            ";

            // Attach the script to the Total field's Calculate event
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
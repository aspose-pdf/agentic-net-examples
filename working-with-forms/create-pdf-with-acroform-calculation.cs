using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the form fields (coordinates are in points)
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(50, 750, 150, 770);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(200, 750, 300, 770);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(350, 750, 450, 770);

            // Quantity field
            TextBoxField qtyField = new TextBoxField(doc, qtyRect);
            qtyField.PartialName = "Quantity";
            qtyField.Value = "0";
            qtyField.ReadOnly = false;
            doc.Form.Add(qtyField, 1); // Add to page 1

            // Price field
            TextBoxField priceField = new TextBoxField(doc, priceRect);
            priceField.PartialName = "Price";
            priceField.Value = "0";
            priceField.ReadOnly = false;
            doc.Form.Add(priceField, 1);

            // Total field (read‑only, will be calculated)
            TextBoxField totalField = new TextBoxField(doc, totalRect);
            totalField.PartialName = "Total";
            totalField.Value = "0";
            totalField.ReadOnly = true;
            doc.Form.Add(totalField, 1);

            // JavaScript that calculates Total = Quantity * Price
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('Price').value;
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

            // Save the PDF
            doc.Save("OrderForm.pdf");
        }

        Console.WriteLine("PDF with AcroForm fields and calculation script created.");
    }
}
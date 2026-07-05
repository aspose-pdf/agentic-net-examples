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
        const string outputPath = "InvoiceWithTotal.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, width, height)
            // Quantity field
            Rectangle qtyRect = new Rectangle(100, 700, 150, 20);
            // Unit Price field
            Rectangle priceRect = new Rectangle(300, 700, 150, 20);
            // Total Price field (calculated)
            Rectangle totalRect = new Rectangle(500, 700, 150, 20);

            // Create Quantity field (NumberField)
            NumberField qtyField = new NumberField(page, qtyRect)
            {
                PartialName = "Quantity",
                Value = "0"
            };
            // Create Unit Price field (NumberField)
            NumberField priceField = new NumberField(page, priceRect)
            {
                PartialName = "UnitPrice",
                Value = "0"
            };
            // Create Total field (NumberField) – this will be calculated
            NumberField totalField = new NumberField(page, totalRect)
            {
                PartialName = "Total",
                ReadOnly = true // user should not edit directly
            };

            // Add fields to the form (page numbers are 1‑based)
            doc.Form.Add(qtyField, 1);
            doc.Form.Add(priceField, 1);
            doc.Form.Add(totalField, 1);

            // Ensure the form recalculates automatically when any field changes
            doc.Form.AutoRecalculate = true;

            // JavaScript to calculate Total = Quantity * UnitPrice
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                // Convert to numbers to avoid string concatenation
                qty = parseFloat(qty);
                price = parseFloat(price);
                if (!isNaN(qty) && !isNaN(price)) {
                    this.getField('Total').value = (qty * price).toFixed(2);
                } else {
                    this.getField('Total').value = '';
                }
            ";

            // Assign the calculation script to the Total field
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated Total field saved to '{outputPath}'.");
    }
}
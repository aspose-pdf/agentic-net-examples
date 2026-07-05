using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "order_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define rectangles for the form fields (llx, lly, urx, ury)
            // Quantity field
            Aspose.Pdf.Rectangle qtyRect = new Aspose.Pdf.Rectangle(50, 750, 150, 770);
            // Price field
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(200, 750, 300, 770);
            // Total field (read‑only)
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(350, 750, 450, 770);

            // Create a TextBox field for Quantity
            TextBoxField qtyField = new TextBoxField(doc, qtyRect)
            {
                PartialName = "Quantity",
                Value = "0",
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Create a TextBox field for Price
            TextBoxField priceField = new TextBoxField(doc, priceRect)
            {
                PartialName = "Price",
                Value = "0.00",
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Create a TextBox field for Total (read‑only)
            TextBoxField totalField = new TextBoxField(doc, totalRect)
            {
                PartialName = "Total",
                Value = "0.00",
                ReadOnly = true,
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // JavaScript that calculates Total = Quantity * Price
            string jsCalc = @"
var qty = this.getField('Quantity').value;
var price = this.getField('Price').value;
var total = parseFloat(qty) * parseFloat(price);
this.getField('Total').value = total.toFixed(2);
";

            // Attach the JavaScript to the OnCalculate event of the Total field.
            // This event is triggered whenever the field value needs to be calculated.
            totalField.Actions.OnCalculate = new JavascriptAction(jsCalc);

            // Add fields to the form on page 1 (page numbers are 1‑based)
            doc.Form.Add(qtyField, 1);
            doc.Form.Add(priceField, 1);
            doc.Form.Add(totalField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}

using System;
using System.Drawing; // Required for DefaultAppearance constructor
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ---------- Create form fields ----------
            // Quantity field
            TextBoxField qtyField = new TextBoxField(doc, new Aspose.Pdf.Rectangle(100, 700, 200, 720));
            qtyField.PartialName = "Quantity";
            qtyField.Value = "0";
            qtyField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(qtyField, 1); // place on page 1

            // Unit Price field
            TextBoxField priceField = new TextBoxField(doc, new Aspose.Pdf.Rectangle(250, 700, 350, 720));
            priceField.PartialName = "Price";
            priceField.Value = "0.00";
            priceField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(priceField, 1);

            // Total field (calculated)
            TextBoxField totalField = new TextBoxField(doc, new Aspose.Pdf.Rectangle(400, 700, 500, 720));
            totalField.PartialName = "Total";
            totalField.Value = "0.00";
            totalField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            // Attach JavaScript to calculate Total = Quantity * Price
            totalField.Actions.OnCalculate = new JavascriptAction(
                "event.value = this.getField('Quantity').value * this.getField('Price').value;");
            doc.Form.Add(totalField, 1);

            // Ensure automatic recalculation of calculated fields
            doc.Form.AutoRecalculate = true;

            // Save the PDF with AcroForm fields and embedded JavaScript
            doc.Save("order_form.pdf");
        }

        Console.WriteLine("PDF with AcroForm fields and calculation JavaScript created successfully.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // existing PDF or blank template
        const string outputPath = "invoice_with_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form object exists
            Form form = doc.Form;

            // ---- Quantity field (NumberField) ----
            // Aspose.Pdf.Rectangle(xLL, yLL, xUR, yUR) – coordinates are in points
            NumberField qtyField = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 700, 150, 720));
            qtyField.PartialName = "Quantity";
            qtyField.AlternateName = "Qty";
            qtyField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            form.Add(qtyField);

            // ---- Unit Price field (NumberField) ----
            NumberField priceField = new NumberField(doc, new Aspose.Pdf.Rectangle(200, 700, 250, 720));
            priceField.PartialName = "UnitPrice";
            priceField.AlternateName = "Price";
            priceField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            form.Add(priceField);

            // ---- Total Price field (TextBoxField) ----
            TextBoxField totalField = new TextBoxField(doc, new Aspose.Pdf.Rectangle(300, 700, 380, 720));
            totalField.PartialName = "Total";
            totalField.AlternateName = "TotalPrice";
            totalField.ReadOnly = true;                     // user should not edit directly
            totalField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            form.Add(totalField);

            // ---- JavaScript calculation ----
            // The calculation is attached to the Total field via its OnCalculate action.
            // It reads the values of Quantity and UnitPrice, multiplies them, and sets the result.
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                // Ensure numeric values; empty fields are treated as 0
                qty = isNaN(qty) ? 0 : parseFloat(qty);
                price = isNaN(price) ? 0 : parseFloat(price);
                this.getField('Total').value = (qty * price).toFixed(2);
            ";
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // Optional: improve performance when filling many fields
            form.AutoRecalculate = true; // default is true; kept for clarity

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated total saved to '{outputPath}'.");
    }
}
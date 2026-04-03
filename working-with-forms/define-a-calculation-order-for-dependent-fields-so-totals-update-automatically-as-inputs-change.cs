using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "calculated_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // ---------- Create form fields ----------
            // Quantity field (numeric input)
            NumberField qtyField = new NumberField(page, new Aspose.Pdf.Rectangle(100, 700, 200, 720))
            {
                Name = "Quantity",
                PartialName = "Quantity",
                Value = "0"
            };
            doc.Form.Add(qtyField);

            // UnitPrice field (numeric input)
            NumberField priceField = new NumberField(page, new Aspose.Pdf.Rectangle(100, 660, 200, 680))
            {
                Name = "UnitPrice",
                PartialName = "UnitPrice",
                Value = "0"
            };
            doc.Form.Add(priceField);

            // Total field (read‑only, calculated)
            NumberField totalField = new NumberField(page, new Aspose.Pdf.Rectangle(100, 620, 200, 640))
            {
                Name = "Total",
                PartialName = "Total",
                ReadOnly = true,
                Value = "0"
            };
            doc.Form.Add(totalField);

            // ---------- Define calculation script ----------
            // JavaScript that multiplies Quantity by UnitPrice and stores the result in Total
            string js = "var qty = this.getField('Quantity').value;" +
                        "var price = this.getField('UnitPrice').value;" +
                        "var total = qty * price;" +
                        "event.value = total;";
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // ---------- Set calculation order ----------
            // Ensure fields are processed in the correct sequence
            doc.Form.CalculatedFields = new List<Field> { qtyField, priceField, totalField };

            // AutoRecalculate is true by default, but set explicitly for clarity
            doc.Form.AutoRecalculate = true;

            // Save the PDF with the form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated fields saved to '{outputPath}'.");
    }
}
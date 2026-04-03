using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "invoice.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, right, top)
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(100, 660, 200, 680);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 620, 200, 640);

            // Quantity field
            NumberField qtyField = new NumberField(page, qtyRect);
            qtyField.PartialName = "Quantity";
            qtyField.Value = "0";

            // Unit price field
            NumberField priceField = new NumberField(page, priceRect);
            priceField.PartialName = "UnitPrice";
            priceField.Value = "0";

            // Total price field (read‑only, calculated)
            NumberField totalField = new NumberField(page, totalRect);
            totalField.PartialName = "Total";
            totalField.ReadOnly = true;

            // JavaScript that multiplies quantity by unit price
            JavascriptAction calcAction = new JavascriptAction(
                "event.value = this.getField('Quantity').value * this.getField('UnitPrice').value;");

            // Attach the calculation script to the Total field
            totalField.Actions.OnCalculate = calcAction;

            // Add fields to the form
            doc.Form.Add(qtyField);
            doc.Form.Add(priceField);
            doc.Form.Add(totalField);

            // Enable automatic recalculation (no explicit Recalculate method exists)
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated total saved to '{outputPath}'.");
    }
}

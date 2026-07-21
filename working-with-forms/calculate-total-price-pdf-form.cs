using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, right, top)
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(100, 660, 200, 680);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 620, 200, 640);

            // Quantity field (associate it with the page)
            NumberField qtyField = new NumberField(page, qtyRect);
            qtyField.Name  = "Quantity";
            qtyField.Value = "1";
            doc.Form.Add(qtyField);

            // Unit price field (associate it with the page)
            NumberField priceField = new NumberField(page, priceRect);
            priceField.Name  = "UnitPrice";
            priceField.Value = "0.00";
            doc.Form.Add(priceField);

            // Total price field (read‑only, calculated)
            NumberField totalField = new NumberField(page, totalRect);
            totalField.Name     = "TotalPrice";
            totalField.ReadOnly = true;

            // JavaScript that multiplies Quantity and UnitPrice
            JavascriptAction calcAction = new JavascriptAction(
                "event.value = this.getField('Quantity').value * this.getField('UnitPrice').value;");
            totalField.Actions.OnCalculate = calcAction;

            doc.Form.Add(totalField);

            // Enable automatic recalculation when any field changes
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated total saved to '{outputPath}'.");
    }
}

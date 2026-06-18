using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "order_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define field rectangles (left, bottom, right, top)
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(50, 700, 150, 720);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(200, 700, 300, 720);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(350, 700, 450, 720);

            // Create text box fields for Quantity, Price and Total
            TextBoxField qtyField = new TextBoxField(doc, qtyRect);
            qtyField.PartialName = "Quantity";
            qtyField.Value = "0";

            TextBoxField priceField = new TextBoxField(doc, priceRect);
            priceField.PartialName = "Price";
            priceField.Value = "0";

            TextBoxField totalField = new TextBoxField(doc, totalRect);
            totalField.PartialName = "Total";
            totalField.ReadOnly = true; // Total should be calculated, not edited

            // Add the fields to the form on page 1
            doc.Form.Add(qtyField, 1);
            doc.Form.Add(priceField, 1);
            doc.Form.Add(totalField, 1);

            // JavaScript that calculates Total = Quantity * Price
            string js = "event.value = this.getField('Quantity').value * this.getField('Price').value;";
            JavascriptAction calcAction = new JavascriptAction(js);
            totalField.Actions.OnCalculate = calcAction;

            // Ensure the form recalculates automatically when any field changes
            doc.Form.AutoRecalculate = true;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
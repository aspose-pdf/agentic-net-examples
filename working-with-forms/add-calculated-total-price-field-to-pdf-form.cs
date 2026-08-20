using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load existing PDF (create rule)
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;

            // Quantity field
            Aspose.Pdf.Rectangle qtyRect = new Aspose.Pdf.Rectangle(100, 700, 150, 720);
            NumberField qtyField = new NumberField(doc, qtyRect)
            {
                PartialName = "Quantity",
                Value = "0"
            };
            form.Add(qtyField);

            // Unit Price field
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(200, 700, 250, 720);
            NumberField priceField = new NumberField(doc, priceRect)
            {
                PartialName = "UnitPrice",
                Value = "0"
            };
            form.Add(priceField);

            // Total Price field (calculated, read‑only)
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(300, 700, 380, 720);
            NumberField totalField = new NumberField(doc, totalRect)
            {
                PartialName = "TotalPrice",
                ReadOnly = true
            };
            // JavaScript to compute total = quantity * unit price
            totalField.Actions.OnCalculate = new JavascriptAction(
                "event.value = this.getField('Quantity').value * this.getField('UnitPrice').value;");
            form.Add(totalField);

            // Example values (could be set from elsewhere)
            qtyField.Value = "5";
            priceField.Value = "12.34";

            // Recalculate calculated fields
            totalField.Recalculate();

            // Save the updated PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
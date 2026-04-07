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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define rectangles for the fields (left, bottom, right, top)
            Aspose.Pdf.Rectangle qtyRect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(220, 700, 320, 720);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(340, 700, 440, 720);

            // Quantity field
            NumberField qtyField = new NumberField(doc, qtyRect)
            {
                Name = "Quantity",
                PartialName = "Quantity",
                Color = Aspose.Pdf.Color.LightGray
            };

            // Unit price field
            NumberField priceField = new NumberField(doc, priceRect)
            {
                Name = "UnitPrice",
                PartialName = "UnitPrice",
                Color = Aspose.Pdf.Color.LightGray
            };

            // Total price field (read‑only, will be calculated)
            NumberField totalField = new NumberField(doc, totalRect)
            {
                Name = "TotalPrice",
                PartialName = "TotalPrice",
                ReadOnly = true,
                Color = Aspose.Pdf.Color.LightGray
            };

            // Add the fields to the form
            doc.Form.Add(qtyField);
            doc.Form.Add(priceField);
            doc.Form.Add(totalField);

            // JavaScript that calculates total = quantity * unit price
            string js = "event.value = this.getField('Quantity').value * this.getField('UnitPrice').value;";
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // Enable automatic recalculation (no Recalculate method exists)
            doc.Form.AutoRecalculate = true;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}

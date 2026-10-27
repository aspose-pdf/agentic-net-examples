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
        const string inputPath = "input.pdf";   // existing PDF to add fields to
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define appearance for the form fields using the constructor (Font, FontSize, TextColor)
            var defaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Quantity field
            TextBoxField qtyField = new TextBoxField(doc)
            {
                PartialName = "Quantity",
                Rect = new Aspose.Pdf.Rectangle(100, 700, 200, 720),
                Value = "0",
                DefaultAppearance = defaultAppearance
            };
            doc.Form.Add(qtyField, 1); // page number, not Page object

            // Unit Price field
            TextBoxField priceField = new TextBoxField(doc)
            {
                PartialName = "UnitPrice",
                Rect = new Aspose.Pdf.Rectangle(100, 660, 200, 680),
                Value = "0.00",
                DefaultAppearance = defaultAppearance
            };
            doc.Form.Add(priceField, 1);

            // Total Price field (read‑only, calculated)
            TextBoxField totalField = new TextBoxField(doc)
            {
                PartialName = "TotalPrice",
                Rect = new Aspose.Pdf.Rectangle(100, 620, 200, 640),
                ReadOnly = true,
                Value = "0.00",
                DefaultAppearance = defaultAppearance
            };
            // JavaScript calculation: Total = Quantity * UnitPrice
            totalField.Actions.OnCalculate = new JavascriptAction(
                "event.value = this.getField('Quantity').value * this.getField('UnitPrice').value;");
            doc.Form.Add(totalField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated total saved to '{outputPath}'.");
    }
}

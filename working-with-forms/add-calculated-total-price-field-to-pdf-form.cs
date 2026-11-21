using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the page exists (use first page for simplicity)
            Page page = doc.Pages[1];

            // Define positions for the fields (left, bottom, right, top)
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(210, 700, 310, 720);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(320, 700, 420, 720);

            // Create a TextBoxField for Quantity
            TextBoxField qtyField = new TextBoxField(page, qtyRect)
            {
                PartialName = "Quantity",
                Value = "0"
            };
            doc.Form.Add(qtyField, 1);

            // Create a TextBoxField for Unit Price
            TextBoxField priceField = new TextBoxField(page, priceRect)
            {
                PartialName = "UnitPrice",
                Value = "0.00"
            };
            doc.Form.Add(priceField, 1);

            // Create a TextBoxField for Total Price
            TextBoxField totalField = new TextBoxField(page, totalRect)
            {
                PartialName = "TotalPrice",
                ReadOnly = true // User should not edit directly
            };
            doc.Form.Add(totalField, 1);

            // Add a JavaScript calculation action to compute Total = Quantity * UnitPrice
            // The script runs whenever either source field changes
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                // Ensure numeric conversion; empty fields become 0
                qty = isNaN(qty) ? 0 : parseFloat(qty);
                price = isNaN(price) ? 0 : parseFloat(price);
                event.value = (qty * price).toFixed(2);
            ";
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated Total Price saved to '{outputPath}'.");
    }
}
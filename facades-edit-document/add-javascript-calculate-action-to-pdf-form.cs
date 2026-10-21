using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Needed for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that recalculates Total = Quantity * Price whenever Quantity changes
            string js = @"
                var qty   = this.getField('Quantity').value;
                var price = this.getField('Price').value;
                // Ensure numeric calculation
                qty   = parseFloat(qty)   || 0;
                price = parseFloat(price) || 0;
                this.getField('Total').value = qty * price;
            ";

            // Attach the script to the Quantity field's calculation event (value change)
            var quantityField = doc.Form["Quantity"] as TextBoxField;
            if (quantityField != null)
            {
                // Use a JavascriptAction and a supported action property (OnCalculate)
                quantityField.Actions.OnCalculate = new JavascriptAction(js);
            }
            else
            {
                Console.Error.WriteLine("Quantity field not found or is not a TextBoxField.");
            }

            // Persist the changes
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}

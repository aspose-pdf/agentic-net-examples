using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_calc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the Quantity field (the Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field before using field members).
            Field quantityField = doc.Form["Quantity"] as Field;
            if (quantityField == null)
            {
                Console.Error.WriteLine("Quantity field not found or not a form field.");
                return;
            }

            // JavaScript that calculates Total = Quantity * UnitPrice
            string jsCode = @"
                var qty = this.getField('Quantity').value;
                var price = this.getField('UnitPrice').value;
                var total = qty * price;
                this.getField('Total').value = total;
            ";

            // Assign the JavaScript to the OnCalculate action of the Quantity field
            quantityField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Ensure automatic recalculation is enabled (default is true)
            doc.Form.AutoRecalculate = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with calculation script: '{outputPath}'.");
    }
}

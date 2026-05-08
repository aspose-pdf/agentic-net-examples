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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the target form field by its name (e.g., "Quantity")
            // The Form indexer returns a WidgetAnnotation, so cast it to Field.
            Field? quantityField = doc.Form["Quantity"] as Field;
            if (quantityField != null)
            {
                // JavaScript that calculates Total = Quantity * Price
                // and assigns the result to the field named "Total"
                string jsCode = @"
                    var qty = this.getField('Quantity').value;
                    var price = this.getField('Price').value;
                    var total = qty * price;
                    this.getField('Total').value = total;
                ";

                // Assign the JavaScript to be executed when the field loses focus
                quantityField.Actions.OnLostFocus = new JavascriptAction(jsCode);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action to '{outputPath}'.");
    }
}
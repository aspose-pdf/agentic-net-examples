using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the target field (e.g., a field named "Quantity")
            // The Form indexer returns a WidgetAnnotation; cast it to a form Field (or WidgetAnnotation) before using.
            Field? quantityField = doc.Form["Quantity"] as Field;
            if (quantityField != null)
            {
                // JavaScript that calculates Total = Quantity * Price when the field loses focus
                string javaScript = @"var qty = this.getField('Quantity').value;
var price = this.getField('Price').value;
this.getField('Total').value = qty * price;";

                // Assign the JavaScript to the OnLostFocus action of the field
                quantityField.Actions.OnLostFocus = new JavascriptAction(javaScript);
            }
            else
            {
                Console.Error.WriteLine("Field 'Quantity' not found in the form.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action to '{outputPath}'.");
    }
}

using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the core API (no need for PdfContentEditor for this task)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the list of field names via the Fields collection
            var fieldNames = doc.Form?.Fields?.Select(f => f.Name).ToArray() ?? Array.Empty<string>();

            // Ensure the required fields exist
            if (!fieldNames.Contains("Quantity") || !fieldNames.Contains("Total"))
            {
                Console.Error.WriteLine("Required fields 'Quantity' or 'Total' are missing.");
                return;
            }

            // Get the Quantity field annotation (it is a widget annotation)
            WidgetAnnotation quantityField = doc.Form["Quantity"] as WidgetAnnotation;
            if (quantityField == null)
            {
                Console.Error.WriteLine("Quantity field is not a widget annotation.");
                return;
            }

            // JavaScript to calculate Total = Quantity * UnitPrice (example unit price = 10)
            string jsCode = "var qty = this.getField('Quantity').value;" +
                            "var price = 10;" +
                            "this.getField('Total').value = qty * price;";

            // Assign the JavaScript to the OnCalculate action of the Quantity field
            quantityField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action: {outputPath}");
    }
}

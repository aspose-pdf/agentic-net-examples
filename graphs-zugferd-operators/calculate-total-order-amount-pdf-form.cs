using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "order_form.pdf";
        const string outputPath = "order_form_with_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF containing the form fields
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;

            // Optional: improve performance when many calculated fields exist
            form.AutoRecalculate = false;

            // JavaScript that sums Qty*Price for line items 1..5 and writes the result to the "Total" field
            string jsCode = @"
var total = 0;
for (var i = 1; i <= 5; i++) {
    var qtyField   = this.getField('Qty' + i);
    var priceField = this.getField('Price' + i);
    if (qtyField != null && priceField != null) {
        var qty   = parseFloat(qtyField.value);
        var price = parseFloat(priceField.value);
        if (!isNaN(qty) && !isNaN(price)) {
            total += qty * price;
        }
    }
}
var totalField = this.getField('Total');
if (totalField != null) {
    totalField.value = total.toFixed(2);
}
";

            // Create a JavaScript action from the script
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Retrieve the "Total" field – the Form indexer returns a WidgetAnnotation,
            // so we must cast it to Aspose.Pdf.Forms.Field before using field‑specific members.
            Field totalField = doc.Form["Total"] as Field;
            if (totalField == null)
            {
                Console.Error.WriteLine("The 'Total' field was not found in the PDF form.");
                return;
            }

            // Execute the JavaScript on the "Total" field (any field can be used as the target)
            totalField.ExecuteFieldJavaScript(jsAction);

            // Force recalculation of the field to ensure the value is updated
            totalField.Recalculate();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with calculated total: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice.pdf";          // PDF with line‑item fields
        const string outputPath = "invoice_with_js.pdf"; // Resulting PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the summary field where the total will be displayed
            // Adjust the field name ("Total") to match the actual field name in the PDF
            Field totalField = form["Total"] as Field; // explicit cast from WidgetAnnotation
            if (totalField == null)
            {
                Console.Error.WriteLine("Summary field 'Total' not found or is not a form field.");
                return;
            }

            // JavaScript that sums the values of line‑item fields and writes the result
            // Adjust the field names ("Item1", "Item2", "Item3") to match your PDF
            string jsCode = @"
                var sum = 0;
                var fields = ['Item1', 'Item2', 'Item3']; // add all line‑item field names here
                for (var i = 0; i < fields.length; i++) {
                    var f = this.getField(fields[i]);
                    if (f && !isNaN(parseFloat(f.value))) {
                        sum += parseFloat(f.value);
                    }
                }
                this.getField('Total').value = sum.toFixed(2);
            ";

            // Create a JavaScript action
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Assign the JavaScript to the OnCalculate action of the summary field
            totalField.Actions.OnCalculate = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript calculation: {outputPath}");
    }
}

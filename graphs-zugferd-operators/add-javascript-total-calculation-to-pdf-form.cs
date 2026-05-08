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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Enable automatic recalculation of calculated fields
            doc.Form.AutoRecalculate = true;

            // JavaScript that sums line‑item fields (Item1, Item2, Item3) and writes the result to the Total field
            string jsCode = @"
var sum = 0;
var fields = ['Item1','Item2','Item3'];
for (var i = 0; i < fields.length; i++) {
    var f = this.getField(fields[i]);
    if (f && !isNaN(parseFloat(f.value))) {
        sum += parseFloat(f.value);
    }
}
var totalField = this.getField('Total');
if (totalField) {
    totalField.value = sum.toFixed(2);
}
";

            // Create a JavaScript action
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Retrieve the "Total" field and cast the returned WidgetAnnotation to a Form Field
            Field totalField = doc.Form["Total"] as Field;
            if (totalField != null)
            {
                // Attach the JavaScript to the summary field's OnCalculate action
                totalField.Actions.OnCalculate = jsAction;
                // Optionally execute immediately to set an initial value
                totalField.ExecuteFieldJavaScript(jsAction);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated total saved to '{outputPath}'.");
    }
}

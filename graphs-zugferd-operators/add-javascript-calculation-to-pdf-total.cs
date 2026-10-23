using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
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
            // JavaScript that sums the values of the price fields and writes the result to the total field
            string js = @"
var total = 0;
var fields = ['price1','price2','price3'];
for (var i = 0; i < fields.length; i++) {
    var f = this.getField(fields[i]);
    if (f != null && f.value != null) {
        var v = parseFloat(f.value);
        if (!isNaN(v)) total += v;
    }
}
var totalField = this.getField('total');
if (totalField != null) totalField.value = total.toFixed(2);
";

            // Names of the line‑item fields that participate in the calculation
            string[] priceFieldNames = new string[] { "price1", "price2", "price3" };

            // Attach the JavaScript action to each price field (triggered on calculate)
            foreach (string fieldName in priceFieldNames)
            {
                TextBoxField field = doc.Form[fieldName] as TextBoxField;
                if (field != null)
                {
                    // Use the correct class name (lower‑case 's') and assign to a valid action property
                    field.Actions.OnCalculate = new JavascriptAction(js);
                }
            }

            // Ensure the summary field exists; create it if it does not
            if (doc.Form["total"] == null)
            {
                TextBoxField totalField = new TextBoxField(
                    doc.Pages[1],
                    new Rectangle(100, 500, 200, 520));
                totalField.PartialName = "total";
                totalField.Value = "0.00";
                doc.Form.Add(totalField);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}

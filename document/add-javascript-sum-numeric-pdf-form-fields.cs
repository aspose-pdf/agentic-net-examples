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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Document‑level JavaScript that sums numeric fields and writes the result to a field named "total"
            string jsFunction = @"
function calculateTotal() {
    var sum = 0;
    var f1 = this.getField('field1');
    if (f1 && !isNaN(parseFloat(f1.value))) sum += parseFloat(f1.value);
    var f2 = this.getField('field2');
    if (f2 && !isNaN(parseFloat(f2.value))) sum += parseFloat(f2.value);
    // Add additional fields here following the same pattern
    this.getField('total').value = sum;
}";

            // Assign the JavaScript as a document‑open action (Document.JavaScript is read‑only)
            doc.OpenAction = new JavascriptAction(jsFunction);

            // Attach the calculation script to each numeric field's OnLostFocus event
            if (doc.Form["field1"] != null)
                doc.Form["field1"].Actions.OnLostFocus = new JavascriptAction("calculateTotal();");

            if (doc.Form["field2"] != null)
                doc.Form["field2"].Actions.OnLostFocus = new JavascriptAction("calculateTotal();");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculation JavaScript saved to '{outputPath}'.");
    }
}

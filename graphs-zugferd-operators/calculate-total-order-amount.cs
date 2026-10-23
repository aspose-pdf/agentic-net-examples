using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Required for JavascriptAction
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "order_form.pdf";   // existing PDF form with line‑item fields
        const string outputPath = "order_form_with_calc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF form
        using (Document pdf = new Document(inputPath))
        {
            // JavaScript that:
            // 1. Calculates each line total (Qty * Price) and stores it in a field named "LineTotal{i}"
            // 2. Sums all line totals into a field named "GrandTotal"
            // Adjust the loop limit (5) to match the actual number of line items in the form.
            string calculateJs = @"
var grandTotal = 0;
for (var i = 1; i <= 5; i++) {
    var qtyField   = this.getField('Qty' + i);
    var priceField = this.getField('Price' + i);
    var lineField  = this.getField('LineTotal' + i);

    var qty   = parseFloat(qtyField.value)   || 0;
    var price = parseFloat(priceField.value) || 0;
    var line  = qty * price;

    lineField.value = line.toFixed(2);
    grandTotal += line;
}
this.getField('GrandTotal').value = grandTotal.toFixed(2);
";

            // Assign the JavaScript to the document's Open action so it runs automatically.
            pdf.OpenAction = new JavascriptAction(calculateJs);

            // Save the modified PDF
            pdf.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculation script saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "order_form.pdf";   // PDF containing line‑item fields
        const string outputPath = "order_form_with_calc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF form inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field that will display the total amount.
            // The field must exist in the PDF and be a TextBoxField.
            TextBoxField totalField = doc.Form["Total"] as TextBoxField;
            if (totalField == null)
            {
                Console.Error.WriteLine("The form does not contain a TextBoxField named 'Total'.");
                return;
            }

            // JavaScript that sums QtyN * PriceN for N = 1..5 and assigns the result to the Total field.
            // The script is attached to the Total field's Calculate action, so it runs automatically
            // whenever any dependent field changes.
            string calculateJs = @"
var total = 0;
for (var i = 1; i <= 5; i++) {
    var qtyField   = this.getField('Qty' + i);
    var priceField = this.getField('Price' + i);
    if (qtyField && priceField) {
        var qty   = parseFloat(qtyField.value);
        var price = parseFloat(priceField.value);
        if (!isNaN(qty) && !isNaN(price)) {
            total += qty * price;
        }
    }
}
event.value = total;
";

            // Assign the JavaScript to the Calculate action of the Total field.
            totalField.Actions.OnCalculate = new JavascriptAction(calculateJs);

            // Save the modified PDF. The Document.Save overload without SaveOptions always writes PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with total calculation saved to '{outputPath}'.");
    }
}

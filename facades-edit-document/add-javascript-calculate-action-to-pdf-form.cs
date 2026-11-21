using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // JavaScript that multiplies Quantity and UnitPrice and stores the result in Total
        string jsCode = @"
var qty   = this.getField('Quantity').value;
var price = this.getField('UnitPrice').value;
if (!isNaN(qty) && !isNaN(price)) {
    this.getField('Total').value = qty * price;
}";

        // Use FormEditor.AddFieldScript to attach the JavaScript to the Quantity field.
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);
            // The script will be executed when the field value changes (on blur/commit).
            editor.AddFieldScript("Quantity", jsCode);
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string buttonName = "CalcButton";

        // JavaScript that reads Quantity and UnitPrice fields, calculates Total, and sets it.
        string jsCode = @"
var qty = this.getField('Quantity').value;
var price = this.getField('UnitPrice').value;
var total = parseFloat(qty) * parseFloat(price);
this.getField('Total').value = total;
";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use FormEditor facade to bind the PDF and add the script to the button.
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);
            // AddFieldScript returns void; no need to capture a boolean.
            formEditor.AddFieldScript(buttonName, jsCode);
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action to '{outputPath}'.");
    }
}

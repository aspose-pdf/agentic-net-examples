using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for PropertyFlag enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor works with the structure of PDF form fields
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document
            formEditor.BindPdf(inputPath);

            // Make the TaxRate field read‑only (use SetFieldAttribute with PropertyFlag.ReadOnly)
            formEditor.SetFieldAttribute("TaxRate", PropertyFlag.ReadOnly);

            // JavaScript that calculates TaxRate from Subtotal (example: 7% tax)
            string js = @"
var subtotal = this.getField('Subtotal').value;
if (isNaN(subtotal)) subtotal = 0;
event.value = subtotal * 0.07;
";

            // Attach the JavaScript to the TaxRate field (use SetFieldScript)
            formEditor.SetFieldScript("TaxRate", js);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only TaxRate field: {outputPath}");
    }
}

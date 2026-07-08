using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Bind the PDF to a FormEditor instance
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Make the "TaxRate" field read‑only
            formEditor.SetFieldAttribute("TaxRate", PropertyFlag.ReadOnly);

            // JavaScript that calculates TaxRate based on Subtotal (e.g., 10% tax)
            string js = @"
                var subtotal = this.getField('Subtotal').value;
                if (subtotal) {
                    this.getField('TaxRate').value = (subtotal * 0.1).toFixed(2);
                }";

            // Attach the script to the "TaxRate" field
            formEditor.AddFieldScript("TaxRate", js);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
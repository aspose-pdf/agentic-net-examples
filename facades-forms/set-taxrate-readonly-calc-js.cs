using System;
using System.IO;
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

        // Open the PDF with FormEditor (facade API)
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPath);

            // Make the "TaxRate" field read‑only
            formEditor.SetFieldAttribute("TaxRate", PropertyFlag.ReadOnly);

            // Add JavaScript to calculate TaxRate = Subtotal * 0.1
            string js = "event.value = this.getField('Subtotal').value * 0.1;";
            formEditor.AddFieldScript("TaxRate", js);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
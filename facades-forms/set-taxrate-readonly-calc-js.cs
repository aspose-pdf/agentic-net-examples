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

        // Initialize FormEditor with source and destination PDFs
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Make the "TaxRate" field read‑only
            formEditor.SetFieldAttribute("TaxRate", PropertyFlag.ReadOnly);

            // Add JavaScript to calculate TaxRate = Subtotal * 0.1
            string js = "event.value = this.getField('Subtotal').value * 0.1;";
            formEditor.SetFieldScript("TaxRate", js);

            // Persist changes to the output file
            formEditor.Save();
        }

        Console.WriteLine($"Form updated and saved to '{outputPath}'.");
    }
}
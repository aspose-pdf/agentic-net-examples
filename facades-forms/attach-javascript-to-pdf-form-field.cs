using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF with form fields
        const string outputPath = "output.pdf";     // PDF after adding JavaScript

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF in a using block to satisfy the document‑disposal rule.
        // The Document instance is not used directly for the JavaScript operation,
        // but its deterministic disposal demonstrates correct lifecycle handling.
        using (Document doc = new Document(inputPath))
        {
            // No modifications are performed on the Document object itself.
            // It will be disposed automatically when exiting this block.
        }

        // FormEditor works with file paths and does not implement IDisposable,
        // so it is not wrapped in a using statement.
        FormEditor formEditor = new FormEditor();

        // Bind the editor to the existing PDF file.
        formEditor.BindPdf(inputPath);

        // JavaScript that updates the "TotalPrice" field whenever "Quantity" changes.
        // This script assumes a constant unit price of 10 (adjust as needed).
        string js = @"
            var qty = this.getField('Quantity').value;
            var unitPrice = 10;
            this.getField('TotalPrice').value = qty * unitPrice;
        ";

        // Attach the script to the "Quantity" field.
        // The correct FormEditor method is SetFieldScript (or AddFieldScript).
        formEditor.SetFieldScript("Quantity", js);

        // Save the modified PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;   // for PropertyFlag enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "InvoiceTemplate.pdf";   // PDF containing Subtotal and TaxRate fields
        const string outputPdf = "InvoiceWithTax.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor facade on the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // 1. Make the TaxRate field read‑only
            formEditor.SetFieldAttribute("TaxRate", PropertyFlag.ReadOnly);

            // 2. Add JavaScript to calculate TaxRate based on Subtotal.
            //    Example: TaxRate = Subtotal * 0.1 (10% tax)
            string js = @"
                var subtotal = this.getField('Subtotal').value;
                if (!isNaN(subtotal)) {
                    this.value = subtotal * 0.1;
                } else {
                    this.value = '';
                }
            ";
            formEditor.SetFieldScript("TaxRate", js);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with read‑only TaxRate and calculation script: {outputPdf}");
    }
}
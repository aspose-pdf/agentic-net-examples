using System;
using System.IO;
using Aspose.Pdf.Facades;          // FormEditor, Form classes
using Aspose.Pdf.Forms;           // PropertyFlag enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF with FormEditor (facade) and edit the form
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF file
            formEditor.BindPdf(inputPdf);

            // 1. Make the "TaxRate" field read‑only
            formEditor.SetFieldAttribute("TaxRate", PropertyFlag.ReadOnly);

            // 2. Add JavaScript that calculates TaxRate from Subtotal (e.g., 10% tax)
            // The script runs when the Subtotal field is edited.
            string js = @"
var subtotal = this.getField('Subtotal').value;
if (subtotal) {
    this.getField('TaxRate').value = subtotal * 0.1;
}";
            // Attach the script to the Subtotal field
            formEditor.AddFieldScript("Subtotal", js);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}

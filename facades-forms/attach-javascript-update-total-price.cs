using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF form
        using (Form form = new Form(inputPdf))
        {
            // JavaScript that updates TotalPrice when Quantity changes
            string js = @"
                var qty = this.getField('Quantity').value;
                var price = 10; // unit price, adjust as needed
                this.getField('TotalPrice').value = qty * price;
            ";

            // Attach the script to the Quantity field using FormEditor
            using (FormEditor editor = new FormEditor(form.Document))
            {
                // Set (or replace) the script for the field
                editor.SetFieldScript("Quantity", js);

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}
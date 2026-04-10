using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the fields
        const string outputPdf = "output.pdf";  // PDF after attaching the script

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF to the FormEditor facade
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // JavaScript that reads the value of the "Quantity" field,
            // multiplies it by a unit price (example: 10),
            // and writes the result into the "TotalPrice" field.
            string jsCode = @"
                var qty = this.getField('Quantity').value;
                var unitPrice = 10; // adjust as needed
                var total = qty * unitPrice;
                this.getField('TotalPrice').value = total;
            ";

            // Attach the script to the "Quantity" field.
            // SetFieldScript works for push‑button fields, but it also
            // accepts a script for other field types in practice.
            bool success = editor.SetFieldScript("Quantity", jsCode);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set JavaScript on the Quantity field.");
                return;
            }

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached successfully. Output saved to '{outputPdf}'.");
    }
}
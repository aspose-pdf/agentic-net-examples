using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice_form_v2.pdf";
        const string outputPath = "required_amount.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable; use using for deterministic disposal
        using (FormEditor editor = new FormEditor())
        {
            // Bind the PDF document to the FormEditor
            editor.BindPdf(inputPath);

            // Set the Required flag on the form field named "Amount"
            // The SetFieldAttribute overload accepts only the field name and the flag.
            editor.SetFieldAttribute("Amount", PropertyFlag.Required);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with required field to '{outputPath}'.");
    }
}

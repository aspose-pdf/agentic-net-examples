using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        // Load the PDF with FormEditor (facade API)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // Make the field read‑only
            editor.SetFieldAttribute("TotalAmount", PropertyFlag.ReadOnly);

            // Apply appearance flags (example: ensure the field is printable)
            // Note: SetFieldAppearance works with AnnotationFlags; actual currency formatting
            // would be handled via the field's default appearance, but here we demonstrate the call.
            editor.SetFieldAppearance("TotalAmount", AnnotationFlags.Print);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field 'TotalAmount' set to read‑only and saved to '{outputPdf}'.");
    }
}
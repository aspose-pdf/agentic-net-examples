using System;
using System.IO;
using Aspose.Pdf.Facades;          // FormEditor, FormFieldFacade, etc.
using Aspose.Pdf;                // PropertyFlag
using Aspose.Pdf.Annotations;    // AnnotationFlags

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

        // Open the PDF with FormEditor (facade API)
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the source PDF file
            formEditor.BindPdf(inputPdf);

            // 1. Make the field "TotalAmount" read‑only
            formEditor.SetFieldAttribute("TotalAmount", PropertyFlag.ReadOnly);

            // 2. Apply appearance flags – set the field to be printable.
            //    Currency formatting is handled by the field’s default appearance;
            //    the appearance flags are set via SetFieldAppearance.
            formEditor.SetFieldAppearance("TotalAmount", AnnotationFlags.Print);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"TotalAmount\" set to read‑only and appearance updated. Saved to '{outputPdf}'.");
    }
}

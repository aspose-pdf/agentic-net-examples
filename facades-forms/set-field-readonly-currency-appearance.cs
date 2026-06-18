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
        const string fieldName = "TotalAmount";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF with FormEditor
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Make the field read‑only
            formEditor.SetFieldAttribute(fieldName, PropertyFlag.ReadOnly);

            // Adjust field appearance (example: ensure the field is printable)
            formEditor.SetFieldAppearance(fieldName, AnnotationFlags.Print);

            // Save the updated PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field '{fieldName}' set to read‑only and appearance updated. Saved to '{outputPdf}'.");
    }
}
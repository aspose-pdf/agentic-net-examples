using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "report.pdf";
        const string outputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the FormEditor facade and bind the PDF
            var formEditor = new FormEditor();
            formEditor.BindPdf(inputPath);

            // "Visible" is the default state; combine only the flags you need.
            // Here we want the field to be printable and read‑only.
            AnnotationFlags flags = AnnotationFlags.Print | AnnotationFlags.ReadOnly;

            // Apply the appearance flags to the "Signature" form field
            formEditor.SetFieldAppearance("Signature", flags);

            // Save the modified PDF
            formEditor.Save(outputPath);

            Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

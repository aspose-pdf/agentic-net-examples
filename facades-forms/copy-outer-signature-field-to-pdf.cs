using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath   = "source.pdf";
        const string templatePath = "template.pdf";
        const string outputPath   = "signed.pdf";
        const string fieldName    = "Signature";
        const int    targetPage   = 1; // 1‑based page index

        if (!File.Exists(sourcePath) || !File.Exists(templatePath))
        {
            Console.Error.WriteLine("Required PDF files not found.");
            return;
        }

        // FormEditor implements IDisposable; use a using block.
        using (var editor = new FormEditor())
        {
            // Bind the source PDF that will receive the field.
            editor.BindPdf(sourcePath);

            // Copy the outer (widget) field named "Signature" from the template PDF onto page 1.
            // The correct method is CopyOuterField, not ImportFormFields.
            editor.CopyOuterField(templatePath, fieldName, targetPage);

            // Save the resulting PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}

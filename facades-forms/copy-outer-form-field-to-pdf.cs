using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath   = "source.pdf";   // PDF to be modified (destination)
        const string templatePath = "template.pdf"; // PDF that contains the field to copy
        const string outputPath   = "dated.pdf";    // Resulting PDF
        const string fieldName    = "Date";        // Field to copy
        const int    targetPage   = 2;              // Page number in destination PDF (1‑based)

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Bind the destination PDF (the one we want to edit).
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(sourcePath);

            // Copy the outer (visible) field named "Date" from the template PDF onto the
            // specified page of the destination PDF. The overload that accepts the template
            // path, field name and target page is the correct one for the current API.
            editor.CopyOuterField(templatePath, fieldName, targetPage);

            // Save the modified PDF to the desired output location.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} and saved as '{outputPath}'.");
    }
}

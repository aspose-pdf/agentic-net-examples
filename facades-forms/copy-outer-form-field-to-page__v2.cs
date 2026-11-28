using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath   = "source.pdf";
        const string templatePath = "template.pdf";
        const string outputPath   = "initials.pdf";
        const string fieldName    = "Initials";
        const int    targetPage   = 4; // 1‑based page index

        // Verify input files exist
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePath}");
            return;
        }
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        try
        {
            // FormEditor does NOT implement IDisposable, so no using block
            FormEditor editor = new FormEditor();

            // Load the destination PDF (source.pdf) into the editor
            editor.BindPdf(sourcePath);

            // Copy the outer field named "Initials" from template.pdf onto page 4 of source.pdf
            // Use CopyOuterField (the correct method name) instead of the non‑existent CopyField.
            editor.CopyOuterField(templatePath, fieldName, targetPage);

            // Save the modified PDF
            editor.Save(outputPath);

            Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

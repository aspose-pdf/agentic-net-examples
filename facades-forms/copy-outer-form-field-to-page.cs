using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath   = "source.pdf";
        const string templatePath = "template.pdf";
        const string outputPath   = "approved.pdf";
        const string fieldName    = "Approval";
        const int    targetPage   = 3;

        // Verify input files exist
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

        try
        {
            // FormEditor does NOT implement IDisposable, so no using block.
            FormEditor formEditor = new FormEditor();

            // Bind the editor to the source PDF.
            formEditor.BindPdf(sourcePath);

            // Copy the outer form field named "Approval" from the template PDF
            // onto page 3 of the bound document.
            // Use CopyOuterField (the correct API) instead of the non‑existent CopyFormField.
            formEditor.CopyOuterField(templatePath, fieldName, targetPage);

            // Save the modified PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

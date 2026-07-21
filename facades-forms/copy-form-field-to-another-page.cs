using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";   // PDF containing the field "TemplateField"
        const string targetPdf = "target.pdf";   // Output PDF where the new field will be placed
        const string originalFieldName = "TemplateField";
        const string newFieldName = "ClonedField";
        const int targetPage = 5;                // Page number (1‑based) for the new field

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDFs
        using (FormEditor formEditor = new FormEditor(sourcePdf, targetPdf))
        {
            // Copy the outer definition of "TemplateField" to page 5 of the target PDF
            formEditor.CopyOuterField(sourcePdf, originalFieldName, targetPage);

            // Rename the copied field to "ClonedField"
            formEditor.RenameField(originalFieldName, newFieldName);

            // Persist changes
            formEditor.Save();
        }

        Console.WriteLine($"Field '{originalFieldName}' copied to page {targetPage} as '{newFieldName}' in '{targetPdf}'.");
    }
}
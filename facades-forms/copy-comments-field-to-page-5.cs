using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the template PDF containing the field,
        // and the output PDF that will contain the copied field.
        const string sourcePath   = "source.pdf";
        const string templatePath = "template.pdf";
        const string outputPath   = "comments.pdf";

        // Verify that the required files exist.
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
            // Create a FormEditor instance (it does NOT implement IDisposable).
            FormEditor formEditor = new FormEditor();

            // Bind the FormEditor to the source PDF.
            formEditor.BindPdf(sourcePath);

            // Copy the outer form field named "Comments" from the template PDF
            // onto page 5 of the bound document.
            // The correct FormEditor method is CopyOuterField, not CopyFields.
            formEditor.CopyOuterField(templatePath, "Comments", 5);

            // Save the modified document.
            formEditor.Save(outputPath);
            // Optionally close the editor (not strictly required for saving).
            formEditor.Close();

            Console.WriteLine($"Field \"Comments\" copied to page 5 and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

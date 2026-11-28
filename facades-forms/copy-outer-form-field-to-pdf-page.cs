using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath   = "source.pdf";
        const string templatePath = "template.pdf";
        const string outputPath   = "stamped.pdf";
        const string fieldName    = "ApprovalStamp";
        const int    targetPage   = 3; // 1‑based page index

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

        // FormEditor does not implement IDisposable in older versions, but using it with a
        // try/finally (or a using block) is safe for newer releases. Here we use a using block
        // which will call Close() automatically.
        using (var formEditor = new FormEditor())
        {
            // Bind the source PDF that will receive the field.
            formEditor.BindPdf(sourcePath);

            // Copy the *outer* field named "ApprovalStamp" from the template PDF onto the
            // specified page of the source PDF. The method is CopyOuterField – there is no
            // ImportFormFields method in the FormEditor API.
            formEditor.CopyOuterField(templatePath, fieldName, targetPage);

            // Save the modified document.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}

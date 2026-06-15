using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";
        const string targetPath   = "Invoice.pdf";
        const string outputPath   = "Invoice_Updated.pdf";

        // Ensure source files exist
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        // Load the template form to retrieve its field names
        Form templateForm = new Form(templatePath);
        try
        {
            // Initialize FormEditor for the target PDF (output will be saved to outputPath)
            using (FormEditor editor = new FormEditor(targetPath, outputPath))
            {
                // Copy each field definition from the template to the target
                foreach (string fieldName in templateForm.FieldNames)
                {
                    editor.CopyOuterField(templatePath, fieldName);
                }

                // Persist changes to the output PDF
                editor.Save();
            }
        }
        finally
        {
            // Release resources held by the template Form object
            templateForm.Close();
        }

        Console.WriteLine($"Field definitions copied to '{outputPath}'.");
    }
}
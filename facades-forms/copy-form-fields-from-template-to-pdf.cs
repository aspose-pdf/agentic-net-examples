using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";
        const string targetPath   = "Invoice.pdf";
        const string outputPath   = "Invoice_WithFields.pdf";

        // Verify that both source files exist
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

        // Load the template to obtain its field names
        using (Form templateForm = new Form(templatePath))
        {
            // Prepare the editor for the target PDF
            using (FormEditor editor = new FormEditor())
            {
                // Bind the target PDF that will receive the fields
                editor.BindPdf(targetPath);

                // Copy each field definition from the template into the target
                foreach (string fieldName in templateForm.FieldNames)
                {
                    editor.CopyOuterField(templatePath, fieldName);
                }

                // Save the modified document to a new file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field definitions copied successfully. Output saved to '{outputPath}'.");
    }
}
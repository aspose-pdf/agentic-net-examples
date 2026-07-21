using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";
        const string targetPath   = "Invoice.pdf";
        const string outputPath   = "Invoice_Updated.pdf";

        if (!File.Exists(templatePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Template or target PDF not found.");
            return;
        }

        // Load the template to enumerate its field names
        using (Form templateForm = new Form(templatePath))
        {
            // Initialize FormEditor for the target PDF and specify the output file
            using (FormEditor editor = new FormEditor(targetPath, outputPath))
            {
                // Copy each field definition from the template to the target
                foreach (string fieldName in templateForm.FieldNames)
                {
                    editor.CopyOuterField(templatePath, fieldName);
                }

                // Save the modified target PDF
                editor.Save();
            }
        }

        Console.WriteLine($"Field definitions copied to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";
        const string targetPath   = "Invoice.pdf";
        const string outputPath   = "Invoice_updated.pdf";

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

        // Load the template form to enumerate its field names
        using (Form templateForm = new Form(templatePath))
        {
            // Create a FormEditor for the target PDF and specify the output file
            FormEditor editor = new FormEditor(targetPath, outputPath);

            // Copy each field definition from the template into the target PDF
            foreach (string fieldName in templateForm.FieldNames)
            {
                editor.CopyOuterField(templatePath, fieldName);
            }

            // Save the modified target PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field definitions copied to '{outputPath}'.");
    }
}
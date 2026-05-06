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

        if (!File.Exists(templatePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Template or target PDF not found.");
            return;
        }

        // Retrieve all field names from the template PDF
        string[] templateFields;
        using (Form templateForm = new Form(templatePath))
        {
            templateFields = templateForm.FieldNames;
        }

        // Open the target PDF with FormEditor and copy each field definition
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(targetPath); // load target PDF

            foreach (string fieldName in templateFields)
            {
                // Copy field definition from the template to the target
                editor.CopyOuterField(templatePath, fieldName);
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field definitions copied to '{outputPath}'.");
    }
}
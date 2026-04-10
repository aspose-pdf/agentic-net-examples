using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";
        const string targetPath   = "Invoice.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target not found: {targetPath}");
            return;
        }

        // Load the template to obtain its field names
        using (Form templateForm = new Form(templatePath))
        {
            // Bind the target PDF to a FormEditor instance
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(targetPath);

                // Copy each field definition from the template to the target
                foreach (string fieldName in templateForm.FieldNames)
                {
                    editor.CopyOuterField(templatePath, fieldName);
                }

                // Save changes back to the target PDF
                editor.Save();
            }
        }

        Console.WriteLine("Field definitions copied successfully.");
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";   // PDF containing the form field definitions
        const string targetPath   = "Invoice.pdf";        // PDF that will receive the fields
        const string outputPath   = "Invoice_WithFields.pdf"; // Resulting PDF

        // Verify that source files exist
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

        // Create a copy of the target PDF that will be modified.
        // This preserves the original Invoice.pdf.
        File.Copy(targetPath, outputPath, overwrite: true);

        // -------------------------------------------------------------------
        // 1. Retrieve the list of field names from the template PDF.
        // -------------------------------------------------------------------
        string[] templateFieldNames;
        using (Form form = new Form())
        {
            form.BindPdf(templatePath);
            templateFieldNames = form.FieldNames; // array of field names defined in the template
        }

        // -------------------------------------------------------------------
        // 2. Copy each field from the template into the output PDF.
        //    FormEditor.CopyOuterField expects the destination page number (int) as the third argument.
        // -------------------------------------------------------------------
        using (FormEditor editor = new FormEditor())
        {
            // Bind the destination PDF (the copy we created above).
            editor.BindPdf(outputPath);

            foreach (var fieldName in templateFieldNames)
            {
                // Copy the field definition from the template PDF into the destination PDF.
                // The third argument is the page number where the field will be placed. Use 1 (first page) as default.
                editor.CopyOuterField(templatePath, fieldName, 1);
            }

            // Save the changes to the destination PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Form fields copied from '{templatePath}' to '{outputPath}'.");
    }
}

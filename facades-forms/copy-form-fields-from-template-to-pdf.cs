using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";   // source PDF with field definitions
        const string targetPath   = "Invoice.pdf";        // PDF to receive the fields
        const string outputPath   = "Invoice_WithFields.pdf";

        // ------------------------------------------------------------
        // 1. Create a template PDF that contains at least one form field.
        // ------------------------------------------------------------
        using (Document templateDoc = new Document())
        {
            // Add a page so the field has a location.
            Page tmplPage = templateDoc.Pages.Add();

            // Create a simple text box field.
            TextBoxField txtField = new TextBoxField(templateDoc, new Rectangle(100, 600, 300, 650))
            {
                PartialName = "CustomerName",
                Value = ""
                // The ToolTip property is not available in this version of Aspose.Pdf, so it is omitted.
            };
            // Add the field to the form collection.
            templateDoc.Form.Add(txtField, 1);

            // Save the template PDF – it will now exist for the copy operation.
            templateDoc.Save(templatePath);
        }

        // ------------------------------------------------------------
        // 2. Create a target PDF (the invoice) that will receive the fields.
        // ------------------------------------------------------------
        using (Document targetDoc = new Document())
        {
            // Add a blank page (or any content you need).
            targetDoc.Pages.Add();
            targetDoc.Save(targetPath);
        }

        // ------------------------------------------------------------
        // 3. Copy field definitions from the template to the target PDF.
        // ------------------------------------------------------------
        // Load the template PDF to retrieve its field names.
        using (Document templateDoc = new Document(templatePath))
        {
            var fieldNames = templateDoc.Form.Fields
                                         .Select(f => f.PartialName)
                                         .ToList();

            // Initialize FormEditor for the target PDF.
            using (FormEditor editor = new FormEditor())
            {
                // Bind the target PDF that will receive the copied fields.
                editor.BindPdf(targetPath);

                // Copy each field definition from the template to the target.
                foreach (string fieldName in fieldNames)
                {
                    // CopyOuterField copies the field with its original page and position.
                    editor.CopyOuterField(templatePath, fieldName);
                }

                // Save the modified PDF to the desired output file.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Fields copied successfully to '{outputPath}'.");
    }
}

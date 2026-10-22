using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";
        const string targetPath   = "Invoice.pdf";
        const string outputPath   = "Invoice_WithFields.pdf";

        // Verify input files exist
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPath}");
            return;
        }

        // Load both PDFs
        Document templateDoc = new Document(templatePath);
        Document targetDoc   = new Document(targetPath);

        // ---------------------------------------------------------------------
        // Copy form field definitions from the template to the target PDF.
        // The Document.Form property is read‑only, so we cannot assign it directly.
        // Instead we iterate over the fields in the template, clone each field
        // (using the ICloneable Clone method) and add the clone to the target
        // document's form collection.
        // ---------------------------------------------------------------------
        foreach (Field templateField in templateDoc.Form.Fields)
        {
            // Clone creates an independent copy of the field (including its
            // appearance, name, type, etc.) so the original template remains untouched.
            Field clonedField = (Field)templateField.Clone();
            targetDoc.Form.Add(clonedField);
        }

        // Save the updated PDF to a new file.
        targetDoc.Save(outputPath);

        Console.WriteLine($"Form fields copied to '{outputPath}'.");
    }
}

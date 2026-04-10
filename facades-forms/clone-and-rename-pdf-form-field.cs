using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdf = "template.pdf";   // PDF that contains the field "TemplateField"
        const string targetPdf = "target.pdf";     // PDF where the new field will be placed
        const string outputPdf = "result.pdf";     // Resulting PDF after the operation

        // Verify that the required files exist
        if (!File.Exists(sourcePdf) || !File.Exists(targetPdf))
        {
            Console.Error.WriteLine("Source or target PDF file not found.");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Copy the outer definition of "TemplateField" from sourcePdf to
        //    targetPdf, placing the new field on page 5 of the target document.
        // -----------------------------------------------------------------
        using (FormEditor formEditor = new FormEditor(targetPdf, outputPdf))
        {
            // CopyOuterField(srcFileName, fieldName, pageNum)
            // pageNum is 1‑based; 5 means the fifth page of the target PDF.
            formEditor.CopyOuterField(sourcePdf, "TemplateField", 5);

            // Persist the changes to outputPdf
            formEditor.Save();
        }

        // -----------------------------------------------------------------
        // 2. Rename the copied field from its original name to "ClonedField".
        // -----------------------------------------------------------------
        using (Form form = new Form(outputPdf))
        {
            // RenameField(oldName, newName)
            form.RenameField("TemplateField", "ClonedField");

            // Save the final document (overwrites outputPdf)
            form.Save(outputPdf);
        }

        Console.WriteLine($"Field cloned and renamed successfully. Output saved to '{outputPdf}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string targetPdf = "target.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Copy the outer definition of "TemplateField" to page 5 of the target PDF
        using (FormEditor editor = new FormEditor(sourcePdf, targetPdf))
        {
            // Page numbers are 1‑based; specify page 5
            editor.CopyOuterField(sourcePdf, "TemplateField", 5);
            editor.Save(); // persist the copied field into targetPdf
        }

        // Rename the copied field to "ClonedField"
        using (Form form = new Form(targetPdf))
        {
            form.RenameField("TemplateField", "ClonedField");
            form.Save(); // overwrite targetPdf with the renamed field
        }

        Console.WriteLine("Field cloned and renamed successfully.");
    }
}
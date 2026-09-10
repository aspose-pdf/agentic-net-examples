using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";   // PDF containing the field "TemplateField"
        const string outputPdf = "cloned.pdf";   // Result PDF with the new field

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // 1. Copy the outer definition of "TemplateField" to page 5 of the output PDF
        using (FormEditor editor = new FormEditor(sourcePdf, outputPdf))
        {
            // CopyOuterField(srcFileName, fieldName, pageNum)
            editor.CopyOuterField(sourcePdf, "TemplateField", 5);
            editor.Save(); // Persist changes to outputPdf
        }

        // 2. Rename the copied field to "ClonedField"
        using (Form form = new Form(outputPdf))
        {
            form.RenameField("TemplateField", "ClonedField");
            form.Save(); // Save the renamed field
        }

        Console.WriteLine($"Field cloned to page 5 as 'ClonedField' in '{outputPdf}'.");
    }
}
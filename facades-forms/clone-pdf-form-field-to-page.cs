using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";   // PDF containing the original field
        const string targetPdf = "target.pdf";   // PDF where the cloned field will be placed

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Initialize FormEditor with the source and the destination PDF files
        using (FormEditor formEditor = new FormEditor(sourcePdf, targetPdf))
        {
            // Copy the outer definition of "TemplateField" to page 5 of the target PDF
            formEditor.CopyOuterField(sourcePdf, "TemplateField", 5);

            // Rename the copied field to "ClonedField"
            formEditor.RenameField("TemplateField", "ClonedField");

            // Persist the changes
            formEditor.Save();
        }

        Console.WriteLine($"Cloned field saved to '{targetPdf}'.");
    }
}
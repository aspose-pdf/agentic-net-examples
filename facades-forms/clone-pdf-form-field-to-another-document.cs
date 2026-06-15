using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";   // PDF containing the original field
        const string targetPdf = "target.pdf";   // PDF where the field will be cloned
        const string outputPdf = "target_updated.pdf"; // Resulting PDF

        // Verify that the input files exist
        if (!File.Exists(sourcePdf) || !File.Exists(targetPdf))
        {
            Console.Error.WriteLine("Source or target PDF file not found.");
            return;
        }

        // Initialize FormEditor with the target document and the desired output file
        using (FormEditor formEditor = new FormEditor(targetPdf, outputPdf))
        {
            // Copy the outer definition of "TemplateField" from sourcePdf to page 5 of the target PDF
            formEditor.CopyOuterField(sourcePdf, "TemplateField", 5);

            // Persist the changes
            formEditor.Save();
        }

        Console.WriteLine($"Cloned field saved to '{outputPdf}'.");
    }
}
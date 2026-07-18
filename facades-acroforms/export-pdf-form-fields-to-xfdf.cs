using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormToXfdf
{
    static void Main()
    {
        const string pdfPath  = "PdfForm.pdf";   // Input PDF containing form fields
        const string xfdfPath = "export.xfdf";   // Destination XFDF file

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade for the PDF
        using (Form form = new Form(pdfPath))
        {
            // Create a file stream for the XFDF output
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form field values to the XFDF stream
                form.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Form fields successfully exported to '{xfdfPath}'.");
    }
}
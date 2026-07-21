using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormDataToFdf
{
    static void Main()
    {
        // Path to the source PDF form
        const string pdfPath = "PdfForm.pdf";

        // Path for the exported FDF file
        const string fdfPath = "export.fdf";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Create a Form facade for the PDF
        using (Form form = new Form(pdfPath))
        {
            // Open a file stream for writing the FDF data
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form fields to the FDF stream
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"Form data exported successfully to '{fdfPath}'.");
    }
}
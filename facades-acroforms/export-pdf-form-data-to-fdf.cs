using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormDataToFdf
{
    static void Main()
    {
        // Path to the source PDF form
        const string pdfPath = "PdfForm.pdf";

        // Path where the exported FDF will be saved
        const string fdfPath = "export.fdf";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Use the Form facade to work with the PDF form
        using (Form form = new Form(pdfPath))
        {
            // Create a file stream for the FDF output
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form fields to the FDF stream
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"Form data exported to FDF file: {fdfPath}");
    }
}
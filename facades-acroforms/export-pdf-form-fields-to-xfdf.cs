using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "PdfForm.pdf";
        const string xfdfPath = "export.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade for the PDF
            using (Form form = new Form(pdfPath))
            {
                // Create the output XFDF file stream
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Export form fields to XFDF
                    form.ExportXfdf(xfdfStream);
                }
                // Form is disposed automatically by the using block
            }

            Console.WriteLine($"Form fields exported successfully to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
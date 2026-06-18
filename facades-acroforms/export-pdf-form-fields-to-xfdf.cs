using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormToXfdf
{
    static void Main()
    {
        // Input PDF containing form fields
        const string pdfPath = "PdfForm.pdf";

        // Output XFDF file path
        const string xfdfPath = "export.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(pdfPath))
            {
                // Create (or overwrite) the XFDF file stream
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Export all form field values to the XFDF stream
                    form.ExportXfdf(xfdfStream);
                }

                // No need to call Save on Form; ExportXfdf writes the XFDF file directly
            }

            Console.WriteLine($"Form data successfully exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;   // Aspose.Pdf namespace contains Document and ExportAnnotationsToXfdf

class Program
{
    static void Main()
    {
        // Local PDF file containing the AcroForm
        const string pdfPath = @"C:\Input\AcroForm.pdf";

        // Destination on a network share (UNC path) where the XFDF (XML) will be saved
        const string xfdfPath = @"\\Server\Share\AcroFormData.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create -> load -> dispose via using)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Export all form annotations (AcroForm fields) to an XFDF file.
                // XFDF is an XML representation of the form data.
                pdfDocument.ExportAnnotationsToXfdf(xfdfPath);
            }

            Console.WriteLine($"AcroForm data successfully exported to: {xfdfPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., I/O, permission issues)
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
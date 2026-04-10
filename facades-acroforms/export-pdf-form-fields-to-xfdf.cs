using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormToXfdf
{
    static void Main()
    {
        // Path to the source PDF form
        const string pdfPath = "PdfForm.pdf";

        // Path where the XFDF file will be written
        const string xfdfPath = "export.xfdf";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Use the Form facade to work with AcroForm fields
        using (Form form = new Form(pdfPath))
        {
            // Create the output file stream for the XFDF data
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form fields to XFDF
                form.ExportXfdf(xfdfStream);
                // Stream will be closed by the using block
            }

            // No need to call Save on the Form; ExportXfdf writes the XFDF file directly
        }

        Console.WriteLine($"Form fields exported to XFDF: {xfdfPath}");
    }
}
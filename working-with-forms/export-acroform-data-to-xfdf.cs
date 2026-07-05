using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Local PDF containing AcroForm fields
        const string pdfPath = @"C:\Input\form.pdf";

        // Destination UNC path on the network share where the XFDF (XML) will be saved
        const string xfdfPath = @"\\networkshare\forms\formdata.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Export AcroForm annotations to XFDF (XML format) directly to the network share
                doc.ExportAnnotationsToXfdf(xfdfPath);
            }

            Console.WriteLine($"AcroForm data successfully exported to: {xfdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
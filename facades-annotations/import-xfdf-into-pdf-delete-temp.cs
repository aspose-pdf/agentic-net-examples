using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, temporary XFDF file, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string xfdfTempPath   = "temp_data.xfdf";
        const string outputPdfPath  = "updated.pdf";

        // Verify that required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(xfdfTempPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfTempPath}");
            return;
        }

        try
        {
            // Bind the source PDF and specify the output file
            using (Form form = new Form(sourcePdfPath, outputPdfPath))
            {
                // Open the XFDF file as a stream and import its field data
                using (FileStream xfdfStream = File.OpenRead(xfdfTempPath))
                {
                    form.ImportXfdf(xfdfStream);
                }

                // Save the PDF with the imported data
                form.Save();
            }

            // Delete the temporary XFDF file after a successful import and save
            File.Delete(xfdfTempPath);
            Console.WriteLine($"Import completed. Temporary XFDF file deleted: {xfdfTempPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}
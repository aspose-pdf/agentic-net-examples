using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with form fields
        const string xfdfTempPath  = "temp.xfdf";   // temporary XFDF file containing field data
        const string outputPdfPath = "output.pdf";  // resulting PDF after import

        // Verify that required files exist before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfTempPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfTempPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with source and destination PDF files
            using (Form form = new Form(inputPdfPath, outputPdfPath))
            {
                // Open the XFDF file as a read‑only stream and import its field values
                using (FileStream xfdfStream = new FileStream(xfdfTempPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXfdf(xfdfStream);
                }

                // Persist the changes to the destination PDF
                form.Save();
            }

            // Cleanup: delete the temporary XFDF file after a successful import/save
            File.Delete(xfdfTempPath);
            Console.WriteLine($"Temporary XFDF file deleted: {xfdfTempPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during XFDF import: {ex.Message}");
        }
    }
}
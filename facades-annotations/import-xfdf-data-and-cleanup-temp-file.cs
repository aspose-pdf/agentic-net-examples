using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF with form fields
        const string xfdfPath      = "temp_data.xfdf"; // temporary XFDF file to import
        const string outputPdfPath = "output.pdf";     // result PDF after import

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Bind the PDF to the Form facade
                using (Form form = new Form(pdfDoc))
                {
                    // Import XFDF data from the temporary file
                    using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                    {
                        form.ImportXfdf(xfdfStream);
                    }

                    // Save the updated PDF to the desired output location
                    form.Save(outputPdfPath);
                }
            }

            // Delete the temporary XFDF file only after successful import and save
            File.Delete(xfdfPath);
            Console.WriteLine($"Import completed. Output saved to '{outputPdfPath}'. Temporary XFDF file deleted.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
            // Optionally keep the XFDF file for troubleshooting
        }
    }
}
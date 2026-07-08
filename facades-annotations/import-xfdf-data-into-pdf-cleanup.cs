using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with form fields
        const string xfdfPath  = "temp.xfdf";   // temporary XFDF file containing field values
        const string outputPdf = "output.pdf";  // destination PDF after import

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with source and target PDF files
            using (Form form = new Form(inputPdf, outputPdf))
            {
                // Open the XFDF file as a read‑only stream
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
                {
                    // Import field values from the XFDF stream into the PDF
                    form.ImportXfdf(xfdfStream);
                }

                // Persist the changes to the target PDF
                form.Save();
            }

            // Delete the temporary XFDF file after a successful import
            File.Delete(xfdfPath);
            Console.WriteLine($"Temporary XFDF file '{xfdfPath}' deleted.");
            Console.WriteLine($"PDF with imported data saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string inputPdfPath = "input.pdf";
        string xfdfImportPath = "import.xfdf";
        string outputPdfPath = "output.pdf";
        string xfdfExportPath = "export.xfdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfImportPath))
        {
            Console.Error.WriteLine($"XFDF file to import not found: {xfdfImportPath}");
            return;
        }

        try
        {
            // ---------- Import XFDF into PDF ----------
            using (Form form = new Form())
            {
                // Load the source PDF
                form.BindPdf(inputPdfPath);

                // Import field values from XFDF
                using (FileStream importStream = File.OpenRead(xfdfImportPath))
                {
                    form.ImportXfdf(importStream);
                }

                // Save the PDF with imported data
                form.Save(outputPdfPath);
            }

            // ---------- Export XFDF from the updated PDF ----------
            using (Form form = new Form())
            {
                // Load the PDF that now contains the imported values
                form.BindPdf(outputPdfPath);

                // Export field values to XFDF
                using (FileStream exportStream = File.Create(xfdfExportPath))
                {
                    form.ExportXfdf(exportStream);
                }
            }

            Console.WriteLine("XFDF import and export completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
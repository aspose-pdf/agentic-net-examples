using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string inputPdfPath = "input.pdf";          // PDF with AcroForm fields
        string inputFdfPath = "data.fdf";           // FDF file containing field values
        string outputPdfPath = "output.pdf";        // PDF after importing FDF data
        string outputFdfPath = "exported.fdf";      // FDF exported from the resulting PDF

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(inputFdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found – {inputFdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the PDF document into the Form facade
            // -------------------------------------------------
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);

                // -------------------------------------------------
                // 2. Import field data from the FDF file
                // -------------------------------------------------
                using (FileStream fdfReadStream = File.OpenRead(inputFdfPath))
                {
                    form.ImportFdf(fdfReadStream);
                }

                // -------------------------------------------------
                // 3. Save the updated PDF to a new file
                // -------------------------------------------------
                form.Save(outputPdfPath);
            }

            // -------------------------------------------------
            // 4. Export field data from the updated PDF back to FDF
            // -------------------------------------------------
            using (Form exportForm = new Form())
            {
                exportForm.BindPdf(outputPdfPath);

                using (FileStream fdfWriteStream = File.Create(outputFdfPath))
                {
                    exportForm.ExportFdf(fdfWriteStream);
                }
            }

            Console.WriteLine("Import and export completed successfully.");
            Console.WriteLine($"Updated PDF saved to: {outputPdfPath}");
            Console.WriteLine($"Exported FDF saved to: {outputFdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
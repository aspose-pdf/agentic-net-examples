using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfTemplatePath = "template.pdf";      // PDF that contains the form fields
        const string fdfInputPath    = "readonly_dir/data.fdf"; // FDF file located in a read‑only folder
        const string outputPdfPath   = "filled_output.pdf";

        // Verify files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(fdfInputPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfInputPath}");
            return;
        }

        try
        {
            // Form implements IDisposable – wrap in using for deterministic disposal
            using (Form form = new Form())
            {
                // Bind the PDF that contains the form fields
                form.BindPdf(pdfTemplatePath);

                // Open the FDF file in read‑only mode (FileAccess.Read)
                using (FileStream fdfStream = new FileStream(fdfInputPath, FileMode.Open, FileAccess.Read))
                {
                    // Import the FDF data into the bound PDF
                    form.ImportFdf(fdfStream);
                }

                // Save the resulting PDF with the imported data
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"FDF data successfully merged into PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
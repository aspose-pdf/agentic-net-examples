using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormB.pdf";
        const string fdfPath = "FormB_form.fdf";

        // Verify input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, wrap in using for deterministic disposal
            using (Form form = new Form())
            {
                // Bind the PDF that contains the form fields
                form.BindPdf(pdfPath);

                // Export the form data to an FDF file – ExportFdf expects a Stream, not a file path
                using (FileStream fdfStream = File.Create(fdfPath))
                {
                    form.ExportFdf(fdfStream);
                }
            }

            Console.WriteLine($"FDF file generated: {fdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormC.pdf";
        const string fdfPath = "FormC_form.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, ensure deterministic cleanup
            using (Form form = new Form())
            {
                // Bind the source PDF containing the form fields
                form.BindPdf(pdfPath);

                // Export the form data to an FDF file – ExportFdf expects a Stream
                using (FileStream fdfStream = File.Create(fdfPath))
                {
                    form.ExportFdf(fdfStream);
                }
            }

            Console.WriteLine($"FDF file generated at '{fdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormE.pdf";
        const string fdfPath = "FormE_form.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block.
            using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form())
            {
                // Bind the PDF document that contains the form fields.
                form.BindPdf(pdfPath);

                // Export the form data to an FDF file. ExportFdf expects a Stream, not a file path.
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

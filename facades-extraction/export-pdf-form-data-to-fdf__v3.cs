using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormA.pdf";
        const string fdfPath = "FormA_form.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Form facade does not implement IDisposable in older versions, so we instantiate it directly.
            Form form = new Form();

            // Bind the PDF that contains the interactive form.
            form.BindPdf(pdfPath);

            // Export the form data to an FDF file. ExportFdf expects a Stream, not a file path.
            using (FileStream fdfStream = File.Create(fdfPath))
            {
                form.ExportFdf(fdfStream);
            }

            Console.WriteLine($"FDF exported to '{fdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

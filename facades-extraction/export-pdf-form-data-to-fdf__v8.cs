using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormF.pdf";
        const string fdfPath = "FormF_form.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable, so wrap it in a using block
        using (Form form = new Form())
        {
            // Bind the PDF that contains the form fields
            form.BindPdf(pdfPath);

            // Export the form data to an FDF file – ExportFdf expects a Stream, not a file path
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"FDF exported to '{fdfPath}'.");
    }
}
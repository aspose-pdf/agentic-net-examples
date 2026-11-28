using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormG.pdf";
        const string fdfPath = "FormG_form.fdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Form is a Facades class; it does NOT implement IDisposable, so no using block
        Form form = new Form();

        // Bind the PDF containing the form fields
        form.BindPdf(pdfPath);

        // Export the form data to an FDF file – ExportFdf expects a Stream, not a file path
        using (FileStream fdfStream = File.Create(fdfPath))
        {
            form.ExportFdf(fdfStream);
        }

        Console.WriteLine($"FDF file generated: {fdfPath}");
    }
}

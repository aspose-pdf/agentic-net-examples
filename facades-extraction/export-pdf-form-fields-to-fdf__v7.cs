using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormH.pdf";
        const string fdfPath = "FormH_form.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Form does not implement IDisposable, so we manage its lifetime manually.
        Form form = new Form();
        try
        {
            // Bind the PDF that contains the interactive form.
            form.BindPdf(pdfPath);

            // Export the form fields to an FDF file. ExportFdf expects a Stream, not a file path.
            using (FileStream fdfStream = File.Create(fdfPath))
            {
                form.ExportFdf(fdfStream);
            }

            Console.WriteLine($"FDF file generated: {fdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Release any resources held by the Form object.
            form.Close();
        }
    }
}

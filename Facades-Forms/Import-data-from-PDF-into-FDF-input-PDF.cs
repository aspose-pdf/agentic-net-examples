using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "output.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(pdfPath))
            {
                // Export the form fields to an FDF file
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportFdf(fdfStream);
                }

                // ExportFdf does not modify the PDF, so no Save() is required
                Console.WriteLine($"FDF exported to '{fdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
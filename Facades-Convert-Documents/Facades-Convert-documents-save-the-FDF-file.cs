using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output FDF file path
        const string fdfPath = "output.fdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Create the Form facade and bind it to the PDF document
        using (Form form = new Form())
        {
            form.BindPdf(pdfPath);

            // Export the form fields to an FDF file
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"FDF file successfully saved to '{fdfPath}'.");
    }
}
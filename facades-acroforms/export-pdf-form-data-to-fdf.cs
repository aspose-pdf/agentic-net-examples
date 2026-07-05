using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "PdfForm.pdf";
        const string fdfPath = "export.fdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the PDF file
        using (Form form = new Form(pdfPath))
        {
            // Create a file stream for the output FDF file
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form fields to the FDF stream
                form.ExportFdf(fdfStream);
                // The using block ensures the stream is closed
            }
        }

        Console.WriteLine($"Form data successfully exported to '{fdfPath}'.");
    }
}
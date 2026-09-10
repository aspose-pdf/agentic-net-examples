using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormToFdf
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "output.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(pdfPath))
        {
            // Create a file stream for the FDF output
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form field data to the FDF stream
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"Form data successfully exported to '{fdfPath}'.");
    }
}
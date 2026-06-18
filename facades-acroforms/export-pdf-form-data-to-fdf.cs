using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";
        const string fdfPath = "output.fdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the PDF file
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
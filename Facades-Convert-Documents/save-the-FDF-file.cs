using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
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
            // Create the output FDF stream
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form fields to the FDF stream
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"FDF file saved to '{fdfPath}'.");
    }
}
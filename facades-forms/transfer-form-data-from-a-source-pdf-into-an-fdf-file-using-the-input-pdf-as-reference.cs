using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputFdfPath = "output.fdf";

        // Verify that the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{sourcePdfPath}'.");
            return;
        }

        // Open the PDF as a Form facade (handles AcroForm fields)
        using (Form pdfForm = new Form(sourcePdfPath))
        {
            // Create a file stream for the destination FDF file
            using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form field data from the PDF into the FDF stream
                pdfForm.ExportFdf(fdfStream);
            }
            // Form implements IDisposable; using ensures proper cleanup
        }

        Console.WriteLine($"Form data successfully exported to '{outputFdfPath}'.");
    }
}
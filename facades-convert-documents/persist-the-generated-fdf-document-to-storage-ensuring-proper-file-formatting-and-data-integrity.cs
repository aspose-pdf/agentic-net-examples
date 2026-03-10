using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF form
        const string pdfPath = "input.pdf";
        // Desired path for the exported FDF file
        const string fdfPath = "export.fdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create a Form object for the PDF. The Form class is part of Aspose.Pdf.Facades.
        using (Form form = new Form(pdfPath))
        {
            // Open a file stream for writing the FDF data.
            // FileMode.Create ensures a new file is created or overwritten.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form fields to the FDF stream.
                form.ExportFdf(fdfStream);
                // Flush to guarantee all buffered data is written to disk.
                fdfStream.Flush();
            }
        }

        Console.WriteLine($"FDF successfully exported to '{fdfPath}'.");
    }
}
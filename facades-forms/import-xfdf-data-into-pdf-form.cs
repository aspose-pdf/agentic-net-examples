using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // Existing PDF with form fields
        const string xfdfPath = "data.xfdf";     // XFDF file containing field values
        const string outputPath = "output.pdf";  // Destination PDF after import

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Form facade handles AcroForm operations.
        // Constructor binds the source PDF and sets the output file name.
        using (Form form = new Form(pdfPath, outputPath))
        {
            // Open XFDF stream and import field values.
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXfdf(xfdfStream);
            }

            // Persist changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, XFDF data file and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string xfdfPath      = "data.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Form facade handles AcroForm operations.
        // Constructor binds the source PDF and sets the destination file.
        using (Form form = new Form(sourcePdfPath, outputPdfPath))
        {
            // Open the XFDF file as a read‑only stream.
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                // Import field values from the XFDF stream.
                // The method matches fields by their full names automatically.
                form.ImportXfdf(xfdfStream);
            }

            // Persist the changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"Form fields imported successfully to '{outputPdfPath}'.");
    }
}
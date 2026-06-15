using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, XFDF data and the resulting PDF
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

        // Use the Form facade to import XFDF field values into the PDF.
        // The Form constructor takes the source PDF and the target PDF file name.
        using (Form form = new Form(sourcePdfPath, outputPdfPath))
        {
            // Open the XFDF file as a read‑only stream.
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                // Import field values; matching is performed by full field names.
                form.ImportXfdf(xfdfStream);
            }

            // Persist the changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"Form fields imported successfully to '{outputPdfPath}'.");
    }
}
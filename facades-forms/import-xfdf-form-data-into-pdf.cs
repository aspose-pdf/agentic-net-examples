using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XFDF file containing form data,
        // and the output PDF where the fields will be imported.
        const string sourcePdfPath = "input.pdf";
        const string xfdfPath      = "data.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist.
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

        // The Form facade works directly with PDF files.
        // It is instantiated with the source PDF and the desired output PDF.
        using (Form form = new Form(sourcePdfPath, outputPdfPath))
        {
            // Open the XFDF file as a read‑only stream.
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                // Import the field values from the XFDF stream.
                // Matching is performed by full field names.
                form.ImportXfdf(xfdfStream);
            }

            // Persist the changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"Form fields imported successfully to '{outputPdfPath}'.");
    }
}
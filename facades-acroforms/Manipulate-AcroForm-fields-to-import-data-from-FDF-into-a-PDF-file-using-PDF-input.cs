using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string fdfPath      = "data.fdf";
        const string outputPdfPath = "output.pdf";

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Form facade binds the source PDF and defines the output PDF
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the FDF file as a read‑only stream
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                // Import field values from the FDF stream into the PDF form
                form.ImportFdf(fdfStream);
            }

            // Persist the changes to the output PDF
            form.Save();
        }

        Console.WriteLine($"FDF data imported successfully to '{outputPdfPath}'.");
    }
}
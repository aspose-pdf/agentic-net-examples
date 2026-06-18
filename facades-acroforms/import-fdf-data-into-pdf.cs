using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string fdfPath       = "data.fdf";

        // Verify that required files exist
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

        // Form facade handles AcroForm operations; it implements IDisposable
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the FDF file as a read‑only stream and import its data
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Persist the changes to the output PDF
            form.Save();
        }

        Console.WriteLine($"Form data imported successfully to '{outputPdfPath}'.");
    }
}
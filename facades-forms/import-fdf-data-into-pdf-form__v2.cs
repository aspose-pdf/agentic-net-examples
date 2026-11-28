using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string basePdfPath = "BaseForm.pdf";
        const string fdfPath = "FormData.fdf";
        const string outputPath = "ImportedFdf.pdf";

        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Form implements IDisposable; use using for deterministic disposal
        using (Form form = new Form())
        {
            // Bind the PDF form template
            form.BindPdf(basePdfPath);

            // Import form data from the FDF file – ImportFdf expects a Stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                form.ImportFdf(fdfStream);
            }

            // Save the populated PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }
}

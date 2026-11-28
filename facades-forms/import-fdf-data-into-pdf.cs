using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string fdfPath    = "data.fdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // Form facade handles AcroForm operations.
            using (Form form = new Form())
            {
                // Bind the existing PDF document.
                form.BindPdf(pdfPath);

                // Import field values from the FDF file (requires a Stream).
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    form.ImportFdf(fdfStream);
                }

                // Save the updated PDF.
                form.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

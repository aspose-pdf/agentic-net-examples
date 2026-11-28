using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string fdfPath    = "fields_import1.fdf";
        const string outputPath = "output061.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Use the Form facade for FDF import/export (FormEditor handles field structure only).
        using (var form = new Form())
        {
            // Bind the existing PDF document.
            form.BindPdf(sourcePath);

            // Import form field values from the external FDF file (stream overload).
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                form.ImportFdf(fdfStream);
            }

            // Save the updated PDF.
            form.Save(outputPath);
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPath}'.");
    }
}

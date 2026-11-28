using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "form_data.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Form implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (Form pdfForm = new Form())
        {
            // Bind the existing PDF document to the Form object.
            pdfForm.BindPdf(inputPdfPath);

            // Export the filled form data to an FDF file.
            // ExportFdf expects a Stream, not a file path.
            using (FileStream fdfStream = File.Create(outputFdfPath))
            {
                pdfForm.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"Form data exported to '{outputFdfPath}'.");
    }
}

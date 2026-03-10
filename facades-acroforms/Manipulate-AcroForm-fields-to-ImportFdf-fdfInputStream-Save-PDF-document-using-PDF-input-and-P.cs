using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fdfPath = "data.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Initialize the Form facade with source and destination PDFs
        using (Form form = new Form(inputPdf, outputPdf))
        {
            // Import field values from the FDF stream
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Persist the changes to the output PDF
            form.Save();
        }

        Console.WriteLine($"FDF data imported and saved to '{outputPdf}'.");
    }
}
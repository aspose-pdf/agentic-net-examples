using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fdfFile = "data.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(fdfFile))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfFile}");
            return;
        }

        // Initialize Form with source and destination PDF files
        Form pdfForm = new Form(inputPdf, outputPdf);

        // Import form field values from the FDF stream
        using (FileStream fdfStream = new FileStream(fdfFile, FileMode.Open, FileAccess.Read))
        {
            pdfForm.ImportFdf(fdfStream);
        }

        // Save the updated PDF
        pdfForm.Save();
        Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
    }
}

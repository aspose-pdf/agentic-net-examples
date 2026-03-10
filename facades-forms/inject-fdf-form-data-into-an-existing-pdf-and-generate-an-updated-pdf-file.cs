using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string fdfPath = "data.fdf";
        const string outputPdfPath = "output.pdf";

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

        // Create a Form facade with source PDF and destination PDF
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the FDF file as a stream and import its data into the PDF
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Save the updated PDF
            form.Save();
        }

        Console.WriteLine($"FDF data imported and saved to '{outputPdfPath}'.");
    }
}
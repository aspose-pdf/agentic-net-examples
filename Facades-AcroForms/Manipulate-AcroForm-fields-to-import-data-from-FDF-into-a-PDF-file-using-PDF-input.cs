using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // PDF with AcroForm fields
        const string inputFdfPath  = "data.fdf";    // FDF file containing field values
        const string outputPdfPath = "output.pdf";  // Resulting PDF with imported data

        // Ensure input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(inputFdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {inputFdfPath}");
            return;
        }

        // Use Form facade to bind the PDF and import FDF data
        using (Form form = new Form(inputPdfPath))
        {
            // Open the FDF stream and import its data into the PDF form
            using (FileStream fdfStream = new FileStream(inputFdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Save the updated PDF to the desired output path
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"FDF data imported and saved to '{outputPdfPath}'.");
    }
}
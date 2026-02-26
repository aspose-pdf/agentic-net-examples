using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination PDF
        const string inputPdfPath  = "source.pdf";
        const string outputPdfPath = "filled.pdf";
        const string fdfPath       = "data.fdf";

        // Ensure the source files exist
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

        // Open the FDF stream for reading
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the Form facade with input and output PDF files
            using (Form form = new Form(inputPdfPath, outputPdfPath))
            {
                // Import field values from the FDF stream into the PDF form
                form.ImportFdf(fdfStream);

                // Save the resulting PDF with populated form fields
                form.Save();
            }
        }

        Console.WriteLine($"Form fields imported from FDF and saved to '{outputPdfPath}'.");
    }
}
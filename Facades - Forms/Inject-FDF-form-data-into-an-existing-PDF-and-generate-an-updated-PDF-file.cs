using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // existing PDF with form fields
        const string fdfPath      = "data.fdf";    // FDF file containing field values
        const string outputPdfPath = "output.pdf"; // PDF after injecting FDF data

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found – {fdfPath}");
            return;
        }

        // Form facade handles AcroForm operations.
        // Constructor binds the source PDF and sets the target output file.
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Import field data from the FDF stream into the PDF.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Persist the changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"FDF data injected successfully. Output saved to '{outputPdfPath}'.");
    }
}
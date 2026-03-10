using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the FDF data file and the resulting PDF
        const string inputPdfPath = "input.pdf";
        const string fdfPath = "data.fdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist before proceeding
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

        // Form facade binds the source PDF and defines the output PDF name
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Import the FDF data into the bound PDF
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Persist the changes to the output PDF file
            form.Save();
        }

        Console.WriteLine($"Successfully imported FDF data and saved to '{outputPdfPath}'.");
    }
}
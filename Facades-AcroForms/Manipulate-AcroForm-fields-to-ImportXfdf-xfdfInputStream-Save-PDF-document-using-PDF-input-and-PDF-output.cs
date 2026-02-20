using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PDF file path
        const string outputPdfPath = "output.pdf";
        // XFDF file path (XML based form data)
        const string xfdfPath = "data.xfdf";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found – {xfdfPath}");
            return;
        }

        try
        {
            // Open the XFDF stream (read‑only)
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            // Create the Form facade and bind the PDF document
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);

                // Import XFDF data into the PDF's AcroForm fields
                form.ImportXfdf(xfdfStream);

                // Save the modified PDF to the output file
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"XFDF data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
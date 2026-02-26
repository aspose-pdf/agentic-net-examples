using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";    // PDF with AcroForm fields
        const string inputXfdfPath  = "data.xfdf";    // XFDF file containing field values
        const string outputPdfPath  = "output.pdf";   // Resulting PDF after import

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(inputXfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found – {inputXfdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Open the XFDF stream and import field data into the PDF
                using (FileStream xfdfStream = File.OpenRead(inputXfdfPath))
                {
                    form.ImportXfdf(xfdfStream);
                }

                // Save the updated PDF to the desired output path
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
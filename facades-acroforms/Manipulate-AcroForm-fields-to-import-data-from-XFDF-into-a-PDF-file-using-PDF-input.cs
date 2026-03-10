using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // Source PDF with AcroForm
        const string xfdfPath      = "data.xfdf";   // XFDF file containing field values
        const string outputPdfPath = "output.pdf"; // Resulting PDF after import

        // Verify that required files exist
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
            // Open the XFDF stream for reading
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                // Initialize the Form facade with the source PDF
                using (Form form = new Form(inputPdfPath))
                {
                    // Import field data from the XFDF stream into the PDF form
                    form.ImportXfdf(xfdfStream);

                    // Save the updated PDF to a new file
                    form.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"XFDF data successfully imported. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}
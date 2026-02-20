using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path (can be any supported source format)
        const string inputPath = "input.pdf";
        // Output PDF file path
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Create the Form facade (used for AcroForm operations and format conversion)
            using (Form form = new Form())
            {
                // Bind the source document. If the source is not a PDF, Aspose.Pdf will
                // perform the necessary conversion based on the file extension.
                form.BindPdf(inputPath);

                // The ConvertTo property can be set to change the output format.
                // Leaving it unset results in PDF output (the default).
                // form.ConvertTo = "docx"; // example for other formats

                // Save the resulting PDF document
                form.Save(outputPath);
            }

            Console.WriteLine($"Document saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
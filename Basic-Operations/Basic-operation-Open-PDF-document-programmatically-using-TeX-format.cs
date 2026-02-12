using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source TeX file
        const string texPath = "sample.tex";
        // Desired output PDF file path
        const string outputPdf = "output.pdf";

        // Verify that the TeX file exists before attempting to load it
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        try
        {
            // Initialize TeX loading options (default settings)
            TeXLoadOptions loadOptions = new TeXLoadOptions();

            // Load the TeX file into a PDF document using the TeXLoadOptions
            using (Document pdfDocument = new Document(texPath, loadOptions))
            {
                // Save the generated PDF document
                pdfDocument.Save(outputPdf);
                Console.WriteLine($"PDF successfully created at: {outputPdf}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
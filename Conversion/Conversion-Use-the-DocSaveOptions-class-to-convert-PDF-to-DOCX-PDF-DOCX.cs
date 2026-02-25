using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including DocSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOCX save options
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Specify the desired output format (DOCX)
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Optional: set recognition mode and other conversion tweaks
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as DOCX using the configured options
                pdfDocument.Save(outputDocxPath, docOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
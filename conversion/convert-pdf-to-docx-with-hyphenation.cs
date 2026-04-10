using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOCX conversion options.
                // - RecognizeBullets and RelativeHorizontalProximity help with layout preservation.
                // - The Mode property was removed in recent Aspose.PDF versions; hyphenation is enabled by default
                //   when the output format is DOCX.
                var saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,   // Export as DOCX (correct enum value)
                    RecognizeBullets = true,                // Better list handling (optional)
                    RelativeHorizontalProximity = 2.5f      // Optional tuning
                };

                // Save the PDF as a DOCX file using the configured options.
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
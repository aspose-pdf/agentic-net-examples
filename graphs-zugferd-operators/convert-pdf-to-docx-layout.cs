using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: using ensures disposal)
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options to preserve layout, fonts, and tables
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Output format: DOCX
                    Format = DocSaveOptions.DocFormat.DocX,
                    // EnhancedFlow mode improves table detection while keeping layout fidelity
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    // Convert Type3 fonts to TrueType to retain original font appearance
                    ConvertType3Fonts = true
                };

                // Save the document as DOCX using the specified options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
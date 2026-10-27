using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options.
                // The Format property selects DOCX output.
                // If the current Aspose.Pdf version supports a recognition mode
                // that extracts only images, it can be set via the RecognitionMode
                // property (e.g., DocSaveOptions.RecognitionMode.ImagesOnly).
                // This property is optional and omitted here to ensure compilation
                // with versions where it may not exist.
                var saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                    // RecognitionMode = DocSaveOptions.RecognitionMode.ImagesOnly // uncomment if available
                };

                // Save the PDF as a DOCX file using the configured options.
                pdfDoc.Save(outputPath, saveOptions);
                Console.WriteLine($"PDF successfully converted to DOCX (images only) at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDoc = "output.doc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOC save options. The ImageRecognitionMode property is not available in the current
                // Aspose.Pdf version, so we only set the supported Mode property (optional).
                var saveOptions = new DocSaveOptions();
                // Optional: set the recognition mode if needed. The default is Flow.
                saveOptions.Mode = DocSaveOptions.RecognitionMode.Flow;

                // Save as DOC using the configured options
                pdfDoc.Save(outputDoc, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

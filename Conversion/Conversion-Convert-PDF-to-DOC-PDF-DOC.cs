using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

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
                // Configure DOC save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the target format (.doc)
                    Format = DocSaveOptions.DocFormat.Doc,
                    // Use the Flow recognition mode for editable output
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet recognition (optional but often useful)
                    RecognizeBullets = true
                };

                // Save the PDF as a DOC file using the specified options
                pdfDocument.Save(outputDocPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputDocPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (including DocSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: creation via constructor, wrapped in using for disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure save options for DOCX output
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the desired Word format (DOCX)
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Optional: choose a recognition mode (Flow gives editable text)
                    Mode = DocSaveOptions.RecognitionMode.Flow
                };

                // Save the document as DOCX (non‑PDF format requires explicit SaveOptions)
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during loading or saving
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
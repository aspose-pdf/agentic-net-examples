using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested, though not needed for conversion

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options – must be passed explicitly
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX output format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use flow recognition mode for better layout preservation
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: improve bullet detection and spacing handling
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as DOCX using the configured options
                pdfDoc.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: '{outputDocx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
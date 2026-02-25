using System;
using System.IO;
using Aspose.Pdf;

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
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options – all options are in Aspose.Pdf namespace
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Explicitly request DOCX format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use flow (reflow) recognition mode for better Word layout
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet detection
                    RecognizeBullets = true,
                    // Adjust horizontal proximity for paragraph detection
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as a DOCX file using the configured options
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
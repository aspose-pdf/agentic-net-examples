using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for DocSaveOptions enums

class PdfToDocxConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOCX save options (enhanced mode + custom settings)
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Output format: DOCX
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Enhanced flow mode supports table recognition
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                    // Convert Type3 fonts to TrueType for editable text
                    ConvertType3Fonts = true,

                    // Recognize bullet lists during conversion
                    RecognizeBullets = true,

                    // Adjust horizontal proximity for word grouping
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the document as DOCX using the configured options (lifecycle: save)
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
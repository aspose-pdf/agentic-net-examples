using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure enhanced conversion options for DOCX output
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX as the target format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use EnhancedFlow mode to improve table recognition
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    // Convert Type3 fonts to TrueType for editable text
                    ConvertType3Fonts = true,
                    // Enable bullet list detection
                    RecognizeBullets = true,
                    // Adjust horizontal proximity for word grouping
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as DOCX using the custom options
                pdfDoc.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
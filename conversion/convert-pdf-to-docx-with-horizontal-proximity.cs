using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure DOCX save options
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Specify the desired output format (DOCX)
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Adjust the relative horizontal proximity used during conversion
                    RelativeHorizontalProximity = 0.3f // float literal required
                };

                // Save the PDF as DOCX using the explicit options
                pdfDoc.Save(outputDocxPath, docOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

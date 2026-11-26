using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure DOCX save options. All SaveOptions classes reside in Aspose.Pdf namespace.
                DocSaveOptions docxOptions = new DocSaveOptions
                {
                    // Specify the desired output format.
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Adjust the proximity threshold for horizontal text grouping.
                    RelativeHorizontalProximity = 0.8f // float literal (suffix 'f')
                };

                // Save the PDF as DOCX using the explicit options.
                pdfDoc.Save(outputDocxPath, docxOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

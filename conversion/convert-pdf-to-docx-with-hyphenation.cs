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
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options. Only the Format property is supported for DOCX conversion.
                // Hyphenation settings are not available on DocSaveOptions; they must be handled after conversion
                // (e.g., with Aspose.Words) or are applied automatically based on language metadata.
                var docOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save the PDF as DOCX using the configured options.
                pdfDoc.Save(outputDocx, docOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

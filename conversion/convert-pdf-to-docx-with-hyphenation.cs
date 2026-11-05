using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // retained for potential future use, not required for current conversion

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
                // Configure DOCX save options – HyphenationOptions is not supported on DocSaveOptions
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save the PDF as DOCX using the configured options
                pdfDoc.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"Conversion completed: {outputDocx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

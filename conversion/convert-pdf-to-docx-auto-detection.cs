using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF file
        const string outputDocx = "output.docx"; // destination DOCX file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX output format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Automatic content detection – use Flow recognition mode
                    Mode = DocSaveOptions.RecognitionMode.Flow
                };

                // Save the PDF as DOCX using the configured options
                pdfDoc.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
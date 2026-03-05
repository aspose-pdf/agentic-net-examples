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
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,          // Output as DOCX
                    Mode = DocSaveOptions.RecognitionMode.Flow,     // Full recognition for editability
                    RecognizeBullets = true                         // Optional: improve bullet detection
                };

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
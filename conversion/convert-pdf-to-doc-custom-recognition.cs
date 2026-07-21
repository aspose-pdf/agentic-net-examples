using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDoc = "output.doc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document pdfDocument = new Document(inputPdf))
            {
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Output format: DOC (not DOCX)
                    Format = DocSaveOptions.DocFormat.Doc,

                    // Custom recognition mode – Flow preserves images.
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Optional: increase image resolution for higher quality.
                    ImageResolutionX = 300,
                    ImageResolutionY = 300
                };

                pdfDocument.Save(outputDoc, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}

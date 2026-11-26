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
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure DocSaveOptions to produce a DOCX file with Flow recognition mode
                var docOptions = new DocSaveOptions();
                docOptions.Mode = DocSaveOptions.RecognitionMode.Flow;

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(outputDocxPath, docOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}

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
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options. The 'Mode' property selects the
                // recognition mode. 'Flow' provides the most accurate layout
                // extraction and preserves footnotes.
                var docOptions = new DocSaveOptions();
                docOptions.Mode = DocSaveOptions.RecognitionMode.Flow;

                // Save as DOCX with the specified options.
                pdfDoc.Save(outputDocx, docOptions);
            }

            Console.WriteLine($"Conversion completed: {outputDocx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
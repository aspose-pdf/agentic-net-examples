using System;
using System.IO;
using Aspose.Pdf; // Save option classes are in the root Aspose.Pdf namespace

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
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Enable enhanced recognition mode to preserve footnotes accurately
                var saveOpts = new DocSaveOptions();
                // Set the recognition mode to Flow for better footnote handling
                saveOpts.Mode = DocSaveOptions.RecognitionMode.Flow;

                pdfDoc.Save(outputDocx, saveOpts);
                Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

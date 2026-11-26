using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure DocSaveOptions for DOCX output with Textbox recognition mode
            var docOptions = new DocSaveOptions();
            docOptions.Format = DocSaveOptions.DocFormat.DocX;               // Target format
            docOptions.Mode = DocSaveOptions.RecognitionMode.Textbox;        // Preserve text boxes

            // Save the document as DOCX using the configured options
            pdfDoc.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}

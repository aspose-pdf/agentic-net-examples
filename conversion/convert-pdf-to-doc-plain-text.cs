using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.doc";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOC save options to extract plain text only
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Set output format to DOC
                    Format = DocSaveOptions.DocFormat.Doc,
                    // Use the Textbox recognition mode for plain text extraction
                    Mode = DocSaveOptions.RecognitionMode.Textbox
                };

                // Save the document as DOC with the specified options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX conversion options.
                var docSaveOptions = new DocSaveOptions();
                // Use enhanced (flow) recognition mode for complex tables and graphics.
                docSaveOptions.Mode = DocSaveOptions.RecognitionMode.Flow;
                // No need to set the format – it is inferred from the .docx extension.

                pdfDoc.Save(outputPath, docSaveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

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
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Set DOCX save options to preserve layout, fonts, and tables
                DocSaveOptions saveOpts = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save as DOCX using the specified options
                pdfDoc.Save(outputPath, saveOpts);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
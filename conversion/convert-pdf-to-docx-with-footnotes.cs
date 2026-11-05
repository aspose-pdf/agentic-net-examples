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
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options. The Format property selects DOCX output.
                // Footnotes and enhanced recognition are handled automatically by the current API.
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save as DOCX using the configured options.
                pdfDoc.Save(outputPath, docOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

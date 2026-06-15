using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure DOCX conversion options. Use the correct DocFormat enum value (DocX) and omit the removed Mode property.
            var saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX, // Output as DOCX
                RecognizeBullets = true               // Optional: improve list handling
            };

            // Save the document as DOCX using the specified options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
    }
}

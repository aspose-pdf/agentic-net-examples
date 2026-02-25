using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.doc";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure DocSaveOptions for DOC output
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the target format (DOC)
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition
                RecognizeBullets = true,
                // Adjust horizontal proximity for word grouping
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a DOC file using the configured options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses, including DocSaveOptions, are in this namespace

class PdfToDocConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output DOC file path
        const string outputDocPath = "output.doc";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the output format as .doc (binary Word format)
                Format = DocSaveOptions.DocFormat.Doc,

                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Adjust horizontal proximity for better text grouping
                RelativeHorizontalProximity = 2.5f,

                // Enable bullet recognition during conversion
                RecognizeBullets = true
            };

            // Save the PDF as a DOC file using the specified options
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocPath}'");
    }
}
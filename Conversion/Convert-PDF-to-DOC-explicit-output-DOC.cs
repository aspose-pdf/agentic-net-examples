using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output DOC file path
        const string docPath = "output.doc";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {pdfPath}");
            return;
        }

        // Load the PDF document, convert and save as DOC
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure DOC save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOC format (binary .doc)
                Format = DocSaveOptions.DocFormat.Doc,

                // Use Flow recognition mode for editable output
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Enable bullet recognition
                RecognizeBullets = true,

                // Adjust horizontal proximity for word detection
                RelativeHorizontalProximity = 2.5f
            };

            // Save the document as DOC using the explicit save options
            pdfDocument.Save(docPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {docPath}");
    }
}
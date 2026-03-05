using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDoc = "output.doc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set up options for DOC (Word 97‑2003) export
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the target format as .doc
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Flow recognition mode for editable output
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: improve bullet detection and spacing handling
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a DOC file using the configured options
            pdfDoc.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDoc}'");
    }
}
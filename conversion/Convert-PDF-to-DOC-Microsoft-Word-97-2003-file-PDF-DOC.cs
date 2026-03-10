using System;
using System.IO;
using Aspose.Pdf;   // Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Desired output DOC (Word 97‑2003) file path
        const string outputDoc = "output.doc";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: creation + loading)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOC save options (must use SaveOptions for non‑PDF output)
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the target format as classic .doc
                    Format = DocSaveOptions.DocFormat.Doc,
                    // Use the Flow recognition mode for editable output
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: improve bullet detection and spacing
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the document as DOC using the configured options (lifecycle: saving)
                pdfDoc.Save(outputDoc, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
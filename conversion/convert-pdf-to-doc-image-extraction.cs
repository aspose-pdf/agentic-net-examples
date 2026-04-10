using System;
using System.IO;
using Aspose.Pdf;

// NOTE: If you encounter CS2012 "Cannot open 'AsposePdfApi.dll' for writing" during build,
// make sure that no previous instance of the compiled executable is still running.
// Close any running process that may have loaded the output DLL or rename the project
// assembly to avoid a name clash with referenced Aspose.Pdf assemblies.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocPath = "output.doc";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block to guarantee disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options with a custom recognition mode.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOC (not DOCX).
                Format = DocSaveOptions.DocFormat.Doc,
                // Use Textbox mode to preserve layout while focusing on images.
                Mode = DocSaveOptions.RecognitionMode.Textbox,
                // Extract only OCR sublayer (images) if the PDF contains OCR data.
                ExtractOcrSublayerOnly = true
            };

            // Save the PDF as a DOC file using the specified options.
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocPath}'");
    }
}

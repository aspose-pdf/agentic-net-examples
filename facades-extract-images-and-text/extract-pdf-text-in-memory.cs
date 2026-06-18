using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfInMemoryTextExtractor
{
    // Extracts all text from a PDF supplied via a MemoryStream
    // and returns the text in a new MemoryStream.
    public static MemoryStream ExtractTextFromPdfStream(MemoryStream pdfInput)
    {
        // Ensure the input stream is positioned at the start.
        pdfInput.Position = 0;

        // Load the PDF document from the input stream.
        using (var document = new Aspose.Pdf.Document(pdfInput))
        {
            // Use the PdfExtractor facade to pull text out of the document.
            using (var extractor = new Aspose.Pdf.Facades.PdfExtractor())
            {
                // Bind the in‑memory document to the extractor.
                extractor.BindPdf(document);

                // Perform text extraction (Unicode encoding is the default).
                extractor.ExtractText();

                // Prepare an output MemoryStream to receive the extracted text.
                var textOutput = new MemoryStream();

                // Write the extracted text into the output stream.
                extractor.GetText(textOutput);

                // Reset the output stream position so it can be read by the caller.
                textOutput.Position = 0;

                // Return the stream containing the extracted text.
                // Caller is responsible for disposing this stream.
                return textOutput;
            }
        }
    }
}

// ---------------------------------------------------------------------------
// Added entry point to satisfy the compiler for an executable project.
// This simple Main method demonstrates how the extractor can be used.
// ---------------------------------------------------------------------------
class Program
{
    static void Main(string[] args)
    {
        // Example usage (optional). Replace "sample.pdf" with an actual PDF path
        // if you want to run this demo.
        // The demo is kept minimal to avoid external dependencies.
        if (args.Length == 0)
        {
            Console.WriteLine("No PDF file path supplied. Demo skipped.");
            return;
        }

        string pdfPath = args[0];
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF file into a MemoryStream.
        using (var pdfStream = new MemoryStream(File.ReadAllBytes(pdfPath)))
        {
            // Extract text into another MemoryStream.
            using (var textStream = PdfInMemoryTextExtractor.ExtractTextFromPdfStream(pdfStream))
            using (var reader = new StreamReader(textStream))
            {
                string extractedText = reader.ReadToEnd();
                Console.WriteLine("--- Extracted Text Start ---");
                Console.WriteLine(extractedText);
                Console.WriteLine("--- Extracted Text End ---");
            }
        }
    }
}

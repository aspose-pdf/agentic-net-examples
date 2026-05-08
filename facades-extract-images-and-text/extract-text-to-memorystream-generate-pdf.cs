using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Extract text from the PDF into a MemoryStream
        using (MemoryStream textStream = new MemoryStream())
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF file
                extractor.BindPdf(inputPdfPath);

                // Extract all text using Unicode encoding
                extractor.ExtractText(Encoding.Unicode);

                // Write extracted text to the MemoryStream
                extractor.GetText(textStream);

                // Reset position for subsequent reading
                textStream.Position = 0;
            }

            // Pass the MemoryStream to another library that creates a PDF from text
            GeneratePdfFromTextStream(textStream, "generated.pdf");
        }
    }

    // Example placeholder that consumes a text stream and creates a PDF.
    // Replace the body with the actual library call as needed.
    static void GeneratePdfFromTextStream(Stream textStream, string outputPdfPath)
    {
        // Use Aspose.Pdf core API to demonstrate handling the stream.
        using (Document doc = new Document())
        {
            // Add a new page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Read all text from the stream
            using (StreamReader reader = new StreamReader(textStream, Encoding.Unicode, true, 1024, leaveOpen: true))
            {
                string allText = reader.ReadToEnd();

                // Add the text as a TextFragment to the page
                TextFragment fragment = new TextFragment(allText);
                page.Paragraphs.Add(fragment);
            }

            // Save the generated PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Generated PDF saved to '{outputPdfPath}'.");
    }
}
using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
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

        // Load the source PDF and extract its text
        using (Document srcDoc = new Document(inputPdfPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            srcDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Prepare a MemoryStream containing the extracted text as simple HTML
            string htmlContent = $"<html><body><pre>{System.Net.WebUtility.HtmlEncode(extractedText)}</pre></body></html>";
            byte[] htmlBytes = Encoding.UTF8.GetBytes(htmlContent);
            using (MemoryStream htmlStream = new MemoryStream(htmlBytes))
            {
                // Load the HTML from the stream into a Document
                using (Document htmlDoc = new Document(htmlStream, new HtmlLoadOptions()))
                {
                    // Convert the HTML Document to PDF and write the result into a MemoryStream
                    using (MemoryStream pdfResultStream = new MemoryStream())
                    {
                        htmlDoc.Save(pdfResultStream, SaveFormat.Pdf);
                        // Reset stream position for the next consumer
                        pdfResultStream.Position = 0;

                        // Pass the PDF stream to another library (placeholder example)
                        // OtherPdfLibrary.GeneratePdfFromStream(pdfResultStream);
                        Console.WriteLine("PDF generation stream is ready for further processing.");
                    }
                }
            }
        }
    }
}

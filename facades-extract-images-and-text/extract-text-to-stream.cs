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
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Extract text from the source PDF into a memory stream
        using (MemoryStream textStream = new MemoryStream())
        {
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPdf);
            extractor.ExtractText(Encoding.Unicode);
            extractor.GetText(textStream);
            textStream.Position = 0;

            // Example of passing the stream to another component that creates a PDF
            // Here we create a new PDF using Aspose.Pdf and add the extracted text.
            using (Document newDoc = new Document())
            {
                Page page = newDoc.Pages.Add();
                string extractedText = Encoding.Unicode.GetString(textStream.ToArray());
                TextFragment fragment = new TextFragment(extractedText);
                page.Paragraphs.Add(fragment);
                newDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Generated PDF saved as '{outputPdf}'.");
    }
}
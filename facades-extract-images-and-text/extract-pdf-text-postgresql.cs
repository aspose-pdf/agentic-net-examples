using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

namespace ExtractPdfTextPostgreSql
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                Page page = sampleDoc.Pages.Add();
                TextFragment fragment = new TextFragment("Sample text for extraction.");
                page.Paragraphs.Add(fragment);
                sampleDoc.Save("input.pdf");
            }

            // Extract text from the PDF using PdfExtractor
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf("input.pdf");
                extractor.ExtractText();

                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    textStream.Position = 0;
                    using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                    {
                        string extractedText = reader.ReadToEnd();

                        // TODO: Insert the extracted text into PostgreSQL.
                        // The example is limited to Aspose.Pdf only, so database code is omitted.
                        // In a real application you would use a PostgreSQL ADO.NET provider (e.g., Npgsql)
                        // to execute an INSERT statement with the extracted text.
                        Console.WriteLine("Extracted text:");
                        Console.WriteLine(extractedText);
                    }
                }
            }
        }
    }
}

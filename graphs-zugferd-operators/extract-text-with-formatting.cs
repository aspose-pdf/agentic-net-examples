using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

namespace ExtractTextExample
{
    public class Program
    {
        public static void Main()
        {
            // Create a sample PDF (self‑contained example)
            using (Document createDoc = new Document())
            {
                Page page = createDoc.Pages.Add();
                TextFragment fragment = new TextFragment("Hello World!\nThis is line 2.\nAnd line 3.");
                page.Paragraphs.Add(fragment);
                createDoc.Save("input.pdf");
            }

            // Extract all text while preserving line breaks and spacing
            using (Document doc = new Document("input.pdf"))
            {
                TextExtractionOptions extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                TextDevice textDevice = new TextDevice(extractionOptions);
                StringBuilder extractedBuilder = new StringBuilder();

                foreach (Page page in doc.Pages)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        textDevice.Process(page, ms);
                        string pageText = Encoding.Unicode.GetString(ms.ToArray());
                        extractedBuilder.Append(pageText);
                        extractedBuilder.Append(Environment.NewLine);
                    }
                }

                string extractedText = extractedBuilder.ToString();
                Console.WriteLine("Extracted Text:");
                Console.WriteLine(extractedText);
            }
        }
    }
}
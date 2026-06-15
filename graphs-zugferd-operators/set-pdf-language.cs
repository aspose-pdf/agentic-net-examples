using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

namespace SetPdfLanguageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document sampleDoc = new Document())
            {
                // Add a blank page (pages are 1‑based)
                sampleDoc.Pages.Add();
                // Save the sample PDF
                sampleDoc.Save("sample.pdf");
            }

            // Open the sample PDF and set its language property
            using (Document doc = new Document("sample.pdf"))
            {
                // Access the tagged content interface
                ITaggedContent taggedContent = doc.TaggedContent;
                // Set the document language (e.g., English – United States)
                taggedContent.SetLanguage("en-US");
                // Save the updated PDF
                doc.Save("output.pdf");
            }
        }
    }
}
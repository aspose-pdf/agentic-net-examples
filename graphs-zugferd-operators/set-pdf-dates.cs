using System;
using Aspose.Pdf;

namespace SetPdfMetadataExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file
            using (Document createDoc = new Document())
            {
                // Add a single blank page (evaluation mode allows up to 4 pages)
                createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Open the PDF and set its creation and modification dates
            using (Document doc = new Document("input.pdf"))
            {
                DateTime now = DateTime.Now;
                doc.Info.CreationDate = now;
                doc.Info.ModDate = now;

                // Save the updated PDF
                doc.Save("output.pdf");
            }
        }
    }
}
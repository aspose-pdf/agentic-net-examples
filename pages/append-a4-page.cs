using System;
using Aspose.Pdf;

namespace AppendA4PageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                // Add a single blank page to the sample PDF
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Load the existing PDF
            using (Document doc = new Document("input.pdf"))
            {
                // Step 3: Append an empty page at the end of the document
                Page newPage = doc.Pages.Add();

                // Step 4: Set the page size to A4 (595 x 842 points)
                newPage.PageInfo.Width = 595.0;
                newPage.PageInfo.Height = 842.0;

                // Step 5: Save the modified document
                doc.Save("output.pdf");
            }
        }
    }
}

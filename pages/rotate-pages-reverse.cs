using System;
using Aspose.Pdf;

namespace RotatePagesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with three pages
            using (Document sampleDoc = new Document())
            {
                // Add three blank pages
                sampleDoc.Pages.Add();
                sampleDoc.Pages.Add();
                sampleDoc.Pages.Add();

                // Save the sample PDF
                sampleDoc.Save("input.pdf");
            }

            // Open the sample PDF and rotate pages in reverse order
            using (Document doc = new Document("input.pdf"))
            {
                int pageCount = doc.Pages.Count;

                // Iterate from the last page to the first page
                for (int i = pageCount; i >= 1; i--)
                {
                    // Apply a 90‑degree clockwise rotation
                    doc.Pages[i].Rotate = Rotation.on90;
                }

                // Save the rotated PDF
                doc.Save("rotated.pdf");
            }
        }
    }
}
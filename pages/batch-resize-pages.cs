using System;
using Aspose.Pdf;

namespace BatchResizePages
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF to work with
            using (Document sampleDoc = new Document())
            {
                // Add a single page with the default size
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Load the PDF that needs to be resized
            using (Document doc = new Document("input.pdf"))
            {
                double targetWidth = 800.0;

                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    double originalWidth = page.PageInfo.Width;
                    double originalHeight = page.PageInfo.Height;

                    double scaleFactor = targetWidth / originalWidth;
                    double targetHeight = originalHeight * scaleFactor;

                    page.SetPageSize(targetWidth, targetHeight);
                }

                doc.Save("output.pdf");
            }
        }
    }
}

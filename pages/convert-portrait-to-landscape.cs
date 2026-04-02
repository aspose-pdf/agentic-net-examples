using System;
using Aspose.Pdf;

namespace AsposePdfExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with a single portrait page
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                // Ensure the page is portrait (default)
                samplePage.PageInfo.IsLandscape = false;
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Load the PDF and convert each page to landscape by swapping MediaBox dimensions
            using (Document doc = new Document("input.pdf"))
            {
                int pageCount = doc.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                    // MediaBox coordinates are double – cast to float for the Rectangle constructor
                    float llx = (float)mediaBox.LLX;
                    float lly = (float)mediaBox.LLY;
                    float urx = (float)mediaBox.URX;
                    float ury = (float)mediaBox.URY;

                    float width = urx - llx;
                    float height = ury - lly;

                    // Swap width and height: new URX = LLX + height, new URY = LLY + width
                    float newUrx = llx + height;
                    float newUry = lly + width;

                    page.MediaBox = new Aspose.Pdf.Rectangle(llx, lly, newUrx, newUry);
                    // Optionally mark the page as landscape for consistency
                    page.PageInfo.IsLandscape = true;
                }

                doc.Save("output.pdf");
            }
        }
    }
}

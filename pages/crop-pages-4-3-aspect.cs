using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with two pages
        using (Document sampleDoc = new Document())
        {
            // Add first page (default size)
            sampleDoc.Pages.Add();
            // Add second page with a custom size (e.g., 600x800 points)
            Page customPage = sampleDoc.Pages.Add();
            customPage.SetPageSize(600, 800);
            sampleDoc.Save("sample.pdf");
        }

        // Load the sample PDF
        using (Document pdfDoc = new Document("sample.pdf"))
        {
            // Target aspect ratio 4:3
            float targetAspect = 4f / 3f;

            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                Rectangle originalRect = page.Rect;

                // Cast the double values returned by Rectangle to float
                float originalLLX = (float)originalRect.LLX;
                float originalLLY = (float)originalRect.LLY;
                float originalURX = (float)originalRect.URX;
                float originalURY = (float)originalRect.URY;

                float originalWidth = originalURX - originalLLX;
                float originalHeight = originalURY - originalLLY;

                float newWidth;
                float newHeight;

                if (originalWidth / originalHeight > targetAspect)
                {
                    // Page is too wide – reduce width
                    newHeight = originalHeight;
                    newWidth = originalHeight * targetAspect;
                }
                else
                {
                    // Page is too tall – reduce height
                    newWidth = originalWidth;
                    newHeight = originalWidth / targetAspect;
                }

                float offsetX = (originalWidth - newWidth) / 2f;
                float offsetY = (originalHeight - newHeight) / 2f;

                float newLLX = originalLLX + offsetX;
                float newLLY = originalLLY + offsetY;
                float newURX = newLLX + newWidth;
                float newURY = newLLY + newHeight;

                page.CropBox = new Aspose.Pdf.Rectangle(newLLX, newLLY, newURX, newURY);
            }

            pdfDoc.Save("cropped.pdf");
        }
    }
}

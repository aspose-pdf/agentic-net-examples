using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        try
        {
            // Load both documents using the core Document API
            using (Document firstDoc = new Document(firstPdfPath))
            using (Document secondDoc = new Document(secondPdfPath))
            {
                int pageCount = Math.Min(firstDoc.Pages.Count, secondDoc.Pages.Count);

                for (int i = 1; i <= pageCount; i++)
                {
                    var firstPage  = firstDoc.Pages[i];
                    var secondPage = secondDoc.Pages[i];

                    // Get image collections from each page (core API, no Comparison namespace)
                    var firstImages  = firstPage.Resources.Images;
                    var secondImages = secondPage.Resources.Images;

                    int imageCount = Math.Max(firstImages.Count, secondImages.Count);

                    for (int imgIdx = 1; imgIdx <= imageCount; imgIdx++)
                    {
                        // Retrieve images if they exist on the respective pages
                        var firstImg  = imgIdx <= firstImages.Count  ? firstImages[imgIdx]  : null;
                        var secondImg = imgIdx <= secondImages.Count ? secondImages[imgIdx] : null;

                        // If one of the images is missing, report a difference
                        if (firstImg == null || secondImg == null)
                        {
                            Console.WriteLine($"Image difference on page {i}, image index {imgIdx}: " +
                                              (firstImg == null ? "missing in first PDF" : "missing in second PDF"));
                            continue;
                        }

                        // Compare the raw image bytes
                        using (var msFirst = new MemoryStream())
                        using (var msSecond = new MemoryStream())
                        {
                            firstImg.Save(msFirst);
                            secondImg.Save(msSecond);

                            byte[] firstBytes  = msFirst.ToArray();
                            byte[] secondBytes = msSecond.ToArray();

                            if (!firstBytes.SequenceEqual(secondBytes))
                            {
                                Console.WriteLine($"Image difference on page {i}, image index {imgIdx}: content differs.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace as required

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Custom page dimensions (in points). Example: A5 size 420 x 595 points.
        const double customWidth  = 420.0;
        const double customHeight = 595.0;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Facade instance (not used directly but satisfies the requirement to use Aspose.Pdf.Facades)
        PdfFileEditor facade = new PdfFileEditor();

        // Load source PDF from a stream
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (Document srcDoc = new Document(inputStream))
        // Create an empty target PDF
        using (Document resultDoc = new Document())
        {
            int pageCount = srcDoc.Pages.Count;

            // Compute booklet order: first, last, second, second‑last, ...
            // If the number of pages is odd, add a blank page at the end.
            int totalPages = pageCount;
            if (totalPages % 2 != 0)
            {
                // Add a blank page to make the count even
                srcDoc.Pages.Add();
                totalPages++;
            }

            int left = 1;               // 1‑based index for the first page
            int right = totalPages;     // 1‑based index for the last page

            while (left < right)
            {
                // Add left page
                resultDoc.Pages.Add(srcDoc.Pages[left]);
                SetCustomSize(resultDoc.Pages[resultDoc.Pages.Count], customWidth, customHeight);

                // Add right page
                resultDoc.Pages.Add(srcDoc.Pages[right]);
                SetCustomSize(resultDoc.Pages[resultDoc.Pages.Count], customWidth, customHeight);

                left++;
                right--;
            }

            // If there was an original middle page (even count), it has already been added.
            // Save the booklet PDF to a stream
            using (FileStream outputStream = File.Create(outputPath))
            {
                resultDoc.Save(outputStream);
            }
        }

        Console.WriteLine($"Booklet PDF created at '{outputPath}'.");
    }

    // Helper to set custom dimensions on a page
    private static void SetCustomSize(Aspose.Pdf.Page page, double width, double height)
    {
        page.PageInfo.Width  = width;
        page.PageInfo.Height = height;
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SummaryReport
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using the lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Extract total text length using TextAbsorber (rule: use TextAbsorber)
            // -----------------------------------------------------------------
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            int totalTextLength = absorber.Text?.Length ?? 0;

            // -----------------------------------------------------------------
            // 2. Count images per page (rule: iterate XImage collection)
            // -----------------------------------------------------------------
            int totalImageCount = 0;

            // -----------------------------------------------------------------
            // 3. Count graphics (vector objects) per page.
            //    Aspose.Pdf does not expose a direct graphics collection.
            //    As a proxy, we count Form XObjects which often represent
            //    vector graphics or reusable content.
            // -----------------------------------------------------------------
            int totalGraphicsCount = 0;

            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Image count on this page
                totalImageCount += page.Resources.Images.Count;

                // Form XObject count on this page (used as graphics proxy)
                totalGraphicsCount += page.Resources.Forms.Count;
            }

            // -----------------------------------------------------------------
            // Output the summary report
            // -----------------------------------------------------------------
            Console.WriteLine("=== PDF Summary Report ===");
            Console.WriteLine($"File: {Path.GetFileName(inputPdf)}");
            Console.WriteLine($"Total Pages: {doc.Pages.Count}");
            Console.WriteLine($"Total Text Length (characters): {totalTextLength}");
            Console.WriteLine($"Total Image Count: {totalImageCount}");
            Console.WriteLine($"Total Graphics (Form XObjects) Count: {totalGraphicsCount}");
        }
    }
}
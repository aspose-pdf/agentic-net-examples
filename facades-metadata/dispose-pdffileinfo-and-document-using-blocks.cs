using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal source PDF so the example can run in an empty sandbox.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // at least one page is required
            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Work with PdfFileInfo and Document inside using blocks to guarantee
        //    proper disposal of unmanaged resources.
        // ---------------------------------------------------------------------
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            // The PdfFileInfo instance can be used for metadata, page count, etc.
            // No explicit Close() call is required – the using block will dispose it.

            using (Document doc = new Document(inputPath))
            {
                // Example operation: add a new blank page.
                doc.Pages.Add();

                // Save the modified document.
                doc.Save(outputPath);
            }
        }
    }
}

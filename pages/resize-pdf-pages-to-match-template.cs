using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // not required here but safe for completeness

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath      = "source.pdf";      // PDF to be resized
        const string referencePdfPath   = "template.pdf";    // PDF whose page size we want to match
        const string outputPdfPath      = "resized_output.pdf";

        // Verify files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(referencePdfPath))
        {
            Console.Error.WriteLine($"Reference template not found: {referencePdfPath}");
            return;
        }

        // Load the reference PDF to obtain its page dimensions
        using (Document referenceDoc = new Document(referencePdfPath))
        {
            // Assume the first page defines the desired size
            Page referencePage = referenceDoc.Pages[1];
            Aspose.Pdf.Rectangle refRect = referencePage.Rect;

            // Calculate width and height (in points)
            double targetWidth  = refRect.URX - refRect.LLX;
            double targetHeight = refRect.URY - refRect.LLY;

            // Create a PageSize instance for the target dimensions
            PageSize targetSize = new PageSize((float)targetWidth, (float)targetHeight);

            // Load the source PDF and resize each of its pages
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Pages collection is 1‑based
                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    Page page = sourceDoc.Pages[i];
                    // Resize the page to match the reference dimensions
                    page.Resize(targetSize);
                }

                // Save the resized document
                sourceDoc.Save(outputPdfPath);
                Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
            }
        }
    }
}
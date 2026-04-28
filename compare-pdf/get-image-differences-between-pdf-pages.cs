using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Load the two PDF documents – lifecycle is handled by using blocks
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Determine how many pages can be compared (use the smaller count)
            int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            if (pageCount == 0)
            {
                Console.WriteLine("No pages available for comparison.");
                return;
            }

            // Create the graphical comparer with default settings
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Iterate through each page pair and obtain image differences
            for (int i = 1; i <= pageCount; i++)
            {
                Page page1 = doc1.Pages[i];
                Page page2 = doc2.Pages[i];

                // Get the difference object for the current pair of pages
                using (ImagesDifference diff = comparer.GetDifference(page1, page2))
                {
                    // Example processing: output basic information about the difference
                    Console.WriteLine($"Page {i}: Difference array length = {diff.Difference.Length}, Height = {diff.Height}, Stride = {diff.Stride}");

                    // Additional processing can be performed here, such as analyzing the byte array,
                    // converting it to an image, or saving it to a custom format.
                }
            }
        }
    }
}
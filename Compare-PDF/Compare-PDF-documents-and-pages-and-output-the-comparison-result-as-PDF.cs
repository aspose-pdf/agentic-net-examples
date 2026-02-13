using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class PdfComparer
{
    static void Main(string[] args)
    {
        // Input PDF files and output file paths
        string pdfPath1 = "input1.pdf";
        string pdfPath2 = "input2.pdf";
        string outputPath = "comparisonResult.pdf";

        // Validate input files
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load the two source documents
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Create the result document
        Document resultDoc = new Document();

        // Determine the maximum number of pages to process
        int maxPages = Math.Max(doc1.Pages.Count, doc2.Pages.Count);

        for (int i = 1; i <= maxPages; i++)
        {
            // Get pages if they exist; otherwise treat as null
            Page page1 = i <= doc1.Pages.Count ? doc1.Pages[i] : null;
            Page page2 = i <= doc2.Pages.Count ? doc2.Pages[i] : null;

            // Import the page from the first document if it exists,
            // otherwise import a blank page (to keep page numbers aligned)
            Page resultPage;
            if (page1 != null)
            {
                resultPage = resultDoc.Pages.Add(page1);
            }
            else
            {
                // Create an empty page with default size
                resultPage = resultDoc.Pages.Add();
            }

            // If both pages exist, compare their textual content
            bool pagesAreEqual = false;
            if (page1 != null && page2 != null)
            {
                // Extract text from both pages
                TextAbsorber absorber1 = new TextAbsorber();
                page1.Accept(absorber1);
                string text1 = absorber1.Text;

                TextAbsorber absorber2 = new TextAbsorber();
                page2.Accept(absorber2);
                string text2 = absorber2.Text;

                pagesAreEqual = string.Equals(text1, text2, StringComparison.Ordinal);
            }

            // If pages differ (or one of them is missing), add a visual marker
            if (!pagesAreEqual)
            {
                // Define a rectangle that covers the whole page
                double llx = 0;
                double lly = 0;
                double urx = resultPage.PageInfo.Width;
                double ury = resultPage.PageInfo.Height;
                Aspose.Pdf.Rectangle diffRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a square annotation (visible rectangle) on the page
                SquareAnnotation diffAnnot = new SquareAnnotation(resultPage, diffRect);
                diffAnnot.Color = Color.Red; // border color
                diffAnnot.Contents = $"Page {i} differs";

                // Initialize the border using the provided rule
                diffAnnot.Border = new Border(diffAnnot)
                {
                    Style = BorderStyle.Solid,
                    Width = 2
                };

                // Add the annotation to the page
                resultPage.Annotations.Add(diffAnnot);
            }
        }

        // Save the result document using the standard save rule
        resultDoc.Save(outputPath);
        Console.WriteLine($"Comparison PDF saved to: {outputPath}");
    }
}
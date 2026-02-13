using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for BorderStyle, Color, etc.
using PdfRectangle = Aspose.Pdf.Rectangle; // disambiguate Rectangle type

class PdfComparer
{
    static void Main(string[] args)
    {
        try
        {
            // Input PDF file paths
            const string pdfPath1 = "input1.pdf";
            const string pdfPath2 = "input2.pdf";
            // Output PDF that will contain the comparison report
            const string outputPath = "ComparisonReport.pdf";

            // Verify that both source files exist
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

            // Load the two documents to be compared
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Create a new document that will hold the comparison results
            Document resultDoc = new Document();

            // Add a single page for the report
            Page reportPage = resultDoc.Pages.Add();

            // Helper to add a line of text to the report page
            void AddReportLine(string text, double yPosition)
            {
                TextFragment tf = new TextFragment(text)
                {
                    // Simple formatting – 12pt Arial
                    TextState = { Font = FontRepository.FindFont("Arial"), FontSize = 12 }
                };
                // Position the fragment (X = 50 points from left, Y = specified)
                tf.Position = new Position(50, yPosition);
                reportPage.Paragraphs.Add(tf);
            }

            double currentY = reportPage.PageInfo.Height - 50; // start near top

            // Compare page counts
            AddReportLine($"Document 1 pages: {doc1.Pages.Count}", currentY);
            currentY -= 20;
            AddReportLine($"Document 2 pages: {doc2.Pages.Count}", currentY);
            currentY -= 30;

            if (doc1.Pages.Count != doc2.Pages.Count)
            {
                AddReportLine("Page count differs.", currentY);
                currentY -= 20;
            }
            else
            {
                AddReportLine("Page count is identical.", currentY);
                currentY -= 20;
            }

            // Compare each page up to the smaller page count
            int minPages = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            for (int i = 1; i <= minPages; i++)
            {
                Page page1 = doc1.Pages[i];
                Page page2 = doc2.Pages[i];

                // Compare page dimensions
                bool sizeEqual = Math.Abs(page1.PageInfo.Width - page2.PageInfo.Width) < 0.01 &&
                                 Math.Abs(page1.PageInfo.Height - page2.PageInfo.Height) < 0.01;

                string sizeInfo = sizeEqual
                    ? $"Page {i}: Size identical ({page1.PageInfo.Width} x {page1.PageInfo.Height})"
                    : $"Page {i}: Size differs (Doc1: {page1.PageInfo.Width}x{page1.PageInfo.Height}, Doc2: {page2.PageInfo.Width}x{page2.PageInfo.Height})";

                AddReportLine(sizeInfo, currentY);
                currentY -= 20;

                // Extract text from both pages
                TextAbsorber absorber1 = new TextAbsorber();
                page1.Accept(absorber1);
                string text1 = absorber1.Text;

                TextAbsorber absorber2 = new TextAbsorber();
                page2.Accept(absorber2);
                string text2 = absorber2.Text;

                // Simple text comparison (ignoring whitespace differences)
                bool textEqual = string.Equals(
                    text1?.Replace("\r", "").Replace("\n", "").Trim(),
                    text2?.Replace("\r", "").Replace("\n", "").Trim(),
                    StringComparison.Ordinal);

                string textInfo = textEqual
                    ? $"Page {i}: Text content identical."
                    : $"Page {i}: Text content differs.";

                AddReportLine(textInfo, currentY);
                currentY -= 30;

                // If there is a difference, add a visual rectangle annotation on the report page
                if (!sizeEqual || !textEqual)
                {
                    // Define a rectangle area for the annotation (simple placeholder)
                    double llx = 40;
                    double lly = currentY - 10;
                    double urx = reportPage.PageInfo.Width - 40;
                    double ury = currentY + 20;

                    PdfRectangle rect = new PdfRectangle(llx, lly, urx, ury);
                    LinkAnnotation diffAnnot = new LinkAnnotation(reportPage, rect)
                    {
                        // Tooltip explaining the difference
                        Contents = $"Differences on page {i}",
                        Color = Color.Red
                    };

                    // Initialize border after the annotation object
                    diffAnnot.Border = new Border(diffAnnot)
                    {
                        Style = BorderStyle.Solid,
                        Width = 1
                    };

                    reportPage.Annotations.Add(diffAnnot);
                    currentY -= 40; // leave space after annotation
                }

                // Prevent the Y coordinate from running off the page
                if (currentY < 50)
                {
                    // Add a new page for remaining report lines
                    reportPage = resultDoc.Pages.Add();
                    currentY = reportPage.PageInfo.Height - 50;
                }
            }

            // Save the comparison report
            resultDoc.Save(outputPath);
            Console.WriteLine($"Comparison report saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

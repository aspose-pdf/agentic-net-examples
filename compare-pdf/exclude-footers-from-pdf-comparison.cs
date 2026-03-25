using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string docPath1 = "doc1.pdf";
        const string docPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(docPath1) || !File.Exists(docPath2))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Assume footer occupies the bottom 50 points of each page.
            // Use dimensions of the first page as a reference.
            Page firstPage = doc1.Pages[1];
            double pageWidth = firstPage.PageInfo.Width;
            double footerHeight = 50; // adjust as needed

            Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                0,               // llx
                0,               // lly (bottom of page)
                pageWidth,       // urx (right edge)
                footerHeight     // ury (height of footer region)
            );

            var options = new SideBySideComparisonOptions
            {
                ExcludeAreas1 = new Aspose.Pdf.Rectangle[] { footerRect },
                ExcludeAreas2 = new Aspose.Pdf.Rectangle[] { footerRect }
            };

            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Comparison saved to '{resultPath}'.");
    }
}
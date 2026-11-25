using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";
        const string diffPdfPath = "diff.pdf";
        const string statsJsonPath = "statistics.json";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDFs – using blocks guarantee disposal.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // -----------------------------------------------------------------
            // 1️⃣ Visual side‑by‑side comparison – creates a PDF that shows the
            //    differences between the two documents.
            // -----------------------------------------------------------------
            var sideOptions = new SideBySideComparisonOptions(); // no OutputPath property
            SideBySidePdfComparer.Compare(doc1, doc2, diffPdfPath, sideOptions);

            // -----------------------------------------------------------------
            // 2️⃣ Text‑based comparison – produces a JSON report that contains the
            //    diff operations (acts as the "statistics" requested).
            // -----------------------------------------------------------------
            var textOptions = new ComparisonOptions(); // default options
            var diffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, textOptions);

            // Write the diff information to a JSON file using Aspose's generator.
            var jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(diffs, statsJsonPath);

            Console.WriteLine($"Diff PDF saved to: {diffPdfPath}");
            Console.WriteLine($"Statistics JSON saved to: {statsJsonPath}");
        }
    }
}

using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "document_en.pdf"; // English sample PDF
        const string pdfPath2 = "document_ru.pdf"; // Russian sample PDF
        const string resultPdf = "comparison_result.pdf";

        // ---------------------------------------------------------------------
        // 1. Create sample PDFs inline so the sandbox has the required files.
        // ---------------------------------------------------------------------
        // English PDF
        using (Document docEn = new Document())
        {
            Page pageEn = docEn.Pages.Add();
            TextFragment tfEn = new TextFragment("Hello World");
            pageEn.Paragraphs.Add(tfEn);
            docEn.Save(pdfPath1);
        }

        // Russian PDF (Unicode text)
        using (Document docRu = new Document())
        {
            Page pageRu = docRu.Pages.Add();
            TextFragment tfRu = new TextFragment("Привет мир"); // "Hello World" in Russian
            pageRu.Paragraphs.Add(tfRu);
            docRu.Save(pdfPath2);
        }

        // ---------------------------------------------------------------------
        // 2. Load the PDFs and perform a flat text comparison.
        // ---------------------------------------------------------------------
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            ComparisonOptions options = new ComparisonOptions(); // defaults enable text comparison

            // Perform comparison; a visual diff PDF is also generated.
            List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdf);

            // Report the detected differences
            Console.WriteLine($"Total differences detected: {differences.Count}");
            foreach (DiffOperation diff in differences)
            {
                // Operation type (Insert, Delete, Replace, etc.) and the text involved.
                Console.WriteLine($"{diff.Operation} – \"{diff.Text}\"");
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    // Usage:
    //   Compare.exe doc <doc1.pdf> <doc2.pdf> <result.pdf>
    //   Compare.exe page <doc1.pdf> <doc2.pdf> <page1> <page2> <result.pdf>
    static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Insufficient arguments.");
            Console.WriteLine("Doc mode:  Compare.exe doc <doc1> <doc2> <result>");
            Console.WriteLine("Page mode: Compare.exe page <doc1> <doc2> <page1> <page2> <result>");
            return;
        }

        string mode = args[0].ToLowerInvariant();

        try
        {
            if (mode == "doc")
            {
                // Compare entire documents and let the API save the result PDF.
                string docPath1 = args[1];
                string docPath2 = args[2];
                string resultPath = args[3];

                if (!File.Exists(docPath1) || !File.Exists(docPath2))
                {
                    Console.WriteLine("One or both input files do not exist.");
                    return;
                }

                using (Document doc1 = new Document(docPath1))
                using (Document doc2 = new Document(docPath2))
                {
                    // Default comparison options; customize if needed.
                    ComparisonOptions options = new ComparisonOptions();

                    // This overload saves the comparison result directly to a PDF file.
                    TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
                }

                Console.WriteLine($"Document comparison saved to '{resultPath}'.");
            }
            else if (mode == "page")
            {
                // Compare specific pages and generate a PDF result from the diff list.
                if (args.Length < 6)
                {
                    Console.WriteLine("Page mode requires page numbers and result path.");
                    return;
                }

                string docPath1 = args[1];
                string docPath2 = args[2];
                int pageNum1 = int.Parse(args[3]); // 1‑based index
                int pageNum2 = int.Parse(args[4]); // 1‑based index
                string resultPath = args[5];

                if (!File.Exists(docPath1) || !File.Exists(docPath2))
                {
                    Console.WriteLine("One or both input files do not exist.");
                    return;
                }

                using (Document doc1 = new Document(docPath1))
                using (Document doc2 = new Document(docPath2))
                {
                    // Validate page numbers.
                    if (pageNum1 < 1 || pageNum1 > doc1.Pages.Count ||
                        pageNum2 < 1 || pageNum2 > doc2.Pages.Count)
                    {
                        Console.WriteLine("Invalid page numbers.");
                        return;
                    }

                    Page page1 = doc1.Pages[pageNum1];
                    Page page2 = doc2.Pages[pageNum2];

                    ComparisonOptions options = new ComparisonOptions();

                    // Perform page‑by‑page text comparison.
                    List<DiffOperation> diffs = TextPdfComparer.ComparePages(page1, page2, options);

                    // Generate a PDF that visualizes the differences.
                    PdfOutputGenerator pdfGenerator = new PdfOutputGenerator();
                    pdfGenerator.GenerateOutput(diffs, resultPath);
                }

                Console.WriteLine($"Page comparison saved to '{resultPath}'.");
            }
            else
            {
                Console.WriteLine("Invalid mode. Use 'doc' or 'page'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Example usage:
        //   Compare whole documents:
        //     Program.exe doc input1.pdf input2.pdf result.pdf
        //   Compare specific pages:
        //     Program.exe page input1.pdf input2.pdf 2 3 result.pdf
        //   where 2 is the page number from the first document and 3 from the second.

        string[] args = Environment.GetCommandLineArgs();
        if (args.Length < 5)
        {
            Console.WriteLine("Insufficient arguments.");
            Console.WriteLine("Doc mode: doc <doc1> <doc2> <output>");
            Console.WriteLine("Page mode: page <doc1> <doc2> <page1> <page2> <output>");
            return;
        }

        string mode = args[1].ToLowerInvariant();

        if (mode == "doc")
        {
            string docPath1 = args[2];
            string docPath2 = args[3];
            string outputPath = args[4];

            if (!File.Exists(docPath1) || !File.Exists(docPath2))
            {
                Console.Error.WriteLine("One of the input files does not exist.");
                return;
            }

            // Compare entire documents side‑by‑side.
            using (Document doc1 = new Document(docPath1))
            using (Document doc2 = new Document(docPath2))
            {
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();
                // Options can be customized here if needed.
                SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
            }

            Console.WriteLine($"Documents compared side‑by‑side saved to '{outputPath}'.");
        }
        else if (mode == "page")
        {
            if (args.Length < 7)
            {
                Console.Error.WriteLine("Page mode requires two page numbers.");
                return;
            }

            string docPath1 = args[2];
            string docPath2 = args[3];
            if (!int.TryParse(args[4], out int pageNum1) ||
                !int.TryParse(args[5], out int pageNum2))
            {
                Console.Error.WriteLine("Invalid page numbers.");
                return;
            }
            string outputPath = args[6];

            if (!File.Exists(docPath1) || !File.Exists(docPath2))
            {
                Console.Error.WriteLine("One of the input files does not exist.");
                return;
            }

            using (Document doc1 = new Document(docPath1))
            using (Document doc2 = new Document(docPath2))
            {
                // Aspose.Pdf uses 1‑based page indexing.
                if (pageNum1 < 1 || pageNum1 > doc1.Pages.Count ||
                    pageNum2 < 1 || pageNum2 > doc2.Pages.Count)
                {
                    Console.Error.WriteLine("Page number out of range.");
                    return;
                }

                Page page1 = doc1.Pages[pageNum1];
                Page page2 = doc2.Pages[pageNum2];

                SideBySideComparisonOptions options = new SideBySideComparisonOptions();
                SideBySidePdfComparer.Compare(page1, page2, outputPath, options);
            }

            Console.WriteLine($"Pages {pageNum1} and {pageNum2} compared side‑by‑side saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Unknown mode. Use 'doc' or 'page'.");
        }
    }
}
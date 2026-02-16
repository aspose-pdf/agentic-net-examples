using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // args length 3  -> compare whole documents
        // args length 5  -> compare specific pages
        // ------------------------------------------------------------
        if (args.Length == 3)
        {
            string docPath1 = args[0];
            string docPath2 = args[1];
            string outputPath = args[2];

            // Validate input files
            if (!File.Exists(docPath1))
            {
                Console.Error.WriteLine($"File not found: {docPath1}");
                return;
            }
            if (!File.Exists(docPath2))
            {
                Console.Error.WriteLine($"File not found: {docPath2}");
                return;
            }

            // Load the two PDF documents
            Document doc1 = new Document(docPath1);
            Document doc2 = new Document(docPath2);

            // Default side‑by‑side options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Compare whole documents and write the result to outputPath
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
        }
        else if (args.Length == 5)
        {
            string docPath1 = args[0];
            if (!int.TryParse(args[1], out int pageNum1) || pageNum1 < 1)
            {
                Console.Error.WriteLine($"Invalid page number: {args[1]}");
                return;
            }
            string docPath2 = args[2];
            if (!int.TryParse(args[3], out int pageNum2) || pageNum2 < 1)
            {
                Console.Error.WriteLine($"Invalid page number: {args[3]}");
                return;
            }
            string outputPath = args[4];

            // Validate input files
            if (!File.Exists(docPath1))
            {
                Console.Error.WriteLine($"File not found: {docPath1}");
                return;
            }
            if (!File.Exists(docPath2))
            {
                Console.Error.WriteLine($"File not found: {docPath2}");
                return;
            }

            // Load the two PDF documents
            Document doc1 = new Document(docPath1);
            Document doc2 = new Document(docPath2);

            // Ensure requested pages exist
            if (pageNum1 > doc1.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNum1} does not exist in {docPath1}");
                return;
            }
            if (pageNum2 > doc2.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNum2} does not exist in {docPath2}");
                return;
            }

            // Retrieve the specific pages (1‑based indexing)
            Page page1 = doc1.Pages[pageNum1];
            Page page2 = doc2.Pages[pageNum2];

            // Default side‑by‑side options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Compare the two pages and write the result to outputPath
            SideBySidePdfComparer.Compare(page1, page2, outputPath, options);
        }
        else
        {
            Console.WriteLine("Usage for whole documents:");
            Console.WriteLine("  program.exe <doc1.pdf> <doc2.pdf> <output.pdf>");
            Console.WriteLine("Usage for specific pages:");
            Console.WriteLine("  program.exe <doc1.pdf> <page1> <doc2.pdf> <page2> <output.pdf>");
        }
    }
}
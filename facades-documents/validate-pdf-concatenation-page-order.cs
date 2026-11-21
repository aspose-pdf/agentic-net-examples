using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated (order matters)
        string[] inputFiles = { "part1.pdf", "part2.pdf", "part3.pdf" };
        const string mergedFile = "merged.pdf";

        // Validate that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // --------------------------------------------------------------------
        // Step 1: Extract text from each source PDF page and store in order.
        // --------------------------------------------------------------------
        List<string> sourcePageTexts = new List<string>();
        foreach (string file in inputFiles)
        {
            using (Document srcDoc = new Document(file))
            {
                // Extract text from all pages of the current source document
                TextAbsorber absorber = new TextAbsorber();
                srcDoc.Pages.Accept(absorber);
                // Split the extracted text into per‑page segments
                // (TextAbsorber returns concatenated text; we re‑extract per page)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    TextAbsorber pageAbsorber = new TextAbsorber();
                    srcDoc.Pages[i].Accept(pageAbsorber);
                    sourcePageTexts.Add(pageAbsorber.Text);
                }
            }
        }

        // --------------------------------------------------------------------
        // Step 2: Concatenate the PDFs using Aspose.Pdf.Facades.PdfFileEditor.
        // --------------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(inputFiles, mergedFile);

        // --------------------------------------------------------------------
        // Step 3: Extract text from the merged PDF page by page.
        // --------------------------------------------------------------------
        List<string> mergedPageTexts = new List<string>();
        using (Document mergedDoc = new Document(mergedFile))
        {
            for (int i = 1; i <= mergedDoc.Pages.Count; i++)
            {
                TextAbsorber pageAbsorber = new TextAbsorber();
                mergedDoc.Pages[i].Accept(pageAbsorber);
                mergedPageTexts.Add(pageAbsorber.Text);
            }
        }

        // --------------------------------------------------------------------
        // Step 4: Validate page order by comparing the two text sequences.
        // --------------------------------------------------------------------
        bool orderPreserved = true;

        if (sourcePageTexts.Count != mergedPageTexts.Count)
        {
            orderPreserved = false;
            Console.WriteLine($"Page count mismatch: source total = {sourcePageTexts.Count}, merged = {mergedPageTexts.Count}");
        }
        else
        {
            for (int i = 0; i < sourcePageTexts.Count; i++)
            {
                // Simple string comparison; trim to ignore trailing whitespace differences
                if (!string.Equals(sourcePageTexts[i].Trim(), mergedPageTexts[i].Trim(), StringComparison.Ordinal))
                {
                    orderPreserved = false;
                    Console.WriteLine($"Page {i + 1} differs between source and merged PDF.");
                    break;
                }
            }
        }

        if (orderPreserved)
            Console.WriteLine("Validation succeeded: concatenated PDF preserves original page order.");
        else
            Console.WriteLine("Validation failed: page order was not preserved.");
    }
}
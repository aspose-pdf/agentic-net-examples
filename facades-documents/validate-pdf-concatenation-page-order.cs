using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files to concatenate
        string[] inputFiles = { "file1.pdf", "file2.pdf" };
        string outputFile = "concatenated.pdf";

        // Verify input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Concatenate PDFs using Document API (avoids PdfFileEditor native process)
        Document concatenatedDoc = new Document();
        foreach (string file in inputFiles)
        {
            Document src = new Document(file);
            // Append all pages from the source document
            concatenatedDoc.Pages.Add(src.Pages);
        }
        concatenatedDoc.Save(outputFile);

        // Extract page texts from each original PDF
        List<string> originalPageTexts = new List<string>();
        foreach (string file in inputFiles)
        {
            originalPageTexts.AddRange(GetPageTexts(file));
        }

        // Extract page texts from the concatenated PDF
        List<string> concatenatedPageTexts = GetPageTexts(outputFile);

        // Validate page count
        if (originalPageTexts.Count != concatenatedPageTexts.Count)
        {
            Console.WriteLine("Validation failed: page count mismatch.");
            Console.WriteLine($"Original total pages: {originalPageTexts.Count}");
            Console.WriteLine($"Concatenated total pages: {concatenatedPageTexts.Count}");
            return;
        }

        // Validate page order by comparing text of each page
        bool orderPreserved = true;
        for (int i = 0; i < originalPageTexts.Count; i++)
        {
            if (originalPageTexts[i] != concatenatedPageTexts[i])
            {
                orderPreserved = false;
                Console.WriteLine($"Mismatch at page {i + 1}");
                break;
            }
        }

        if (orderPreserved)
        {
            Console.WriteLine("Validation succeeded: page order preserved after concatenation.");
        }
        else
        {
            Console.WriteLine("Validation failed: page order not preserved.");
        }
    }

    // Returns a list of text contents for each page of the specified PDF
    private static List<string> GetPageTexts(string pdfPath)
    {
        List<string> pageTexts = new List<string>();
        using (Document doc = new Document(pdfPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[i].Accept(absorber);
                pageTexts.Add(absorber.Text ?? string.Empty);
            }
        }
        return pageTexts;
    }
}

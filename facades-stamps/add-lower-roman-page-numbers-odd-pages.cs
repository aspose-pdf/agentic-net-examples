using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine the odd‑numbered pages (1‑based indexing)
        int[] oddPages;
        using (Document tempDoc = new Document(inputPath))
        {
            var oddList = new List<int>();
            for (int i = 1; i <= tempDoc.Pages.Count; i++)
            {
                if (i % 2 == 1) // odd page
                    oddList.Add(i);
            }
            oddPages = oddList.ToArray();
        }

        // Initialize the facade, bind the source PDF and configure the stamp
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);                                 // load source PDF
        fileStamp.NumberingStyle = NumberingStyle.NumeralsRomanLowercase; // Lower‑Roman numbering

        // Add a page number stamp only to odd pages. The overload AddPageNumber(string, int)
        // applies the stamp to the specified page number.
        foreach (int pageNumber in oddPages)
        {
            fileStamp.AddPageNumber("#", pageNumber);
        }

        // Save the stamped PDF
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Page numbers (lower‑roman) added to odd pages → '{outputPath}'.");
    }
}

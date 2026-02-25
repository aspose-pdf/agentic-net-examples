using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM files (treated as source PDFs after loading)
        const string firstCgm  = "first.cgm";
        const string secondCgm = "second.cgm";
        const string outputPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstCgm) || !File.Exists(secondCgm))
        {
            Console.Error.WriteLine("One or more input CGM files not found.");
            return;
        }

        // Load CGM files using CgmLoadOptions (CGM is input‑only)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Merge using nested using blocks to ensure deterministic disposal
        using (Document target = new Document(firstCgm, loadOptions))
        using (Document source = new Document(secondCgm, loadOptions))
        {
            // Append all pages from source to target
            target.Pages.Add(source.Pages);

            // Save the merged result as PDF
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}
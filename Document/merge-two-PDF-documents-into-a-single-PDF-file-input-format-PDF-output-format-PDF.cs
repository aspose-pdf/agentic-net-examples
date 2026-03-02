using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the merged output file
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string mergedPdfPath = "merged.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {secondPdfPath}");
            return;
        }

        try
        {
            // Load the first PDF as the target document
            using (Document target = new Document(firstPdfPath))
            // Load the second PDF as the source document
            using (Document source = new Document(secondPdfPath))
            {
                // Append all pages from the source document to the target document
                target.Pages.Add(source.Pages);

                // Save the merged document to the specified output path
                target.Save(mergedPdfPath);
            }

            Console.WriteLine($"Successfully merged PDFs into '{mergedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}
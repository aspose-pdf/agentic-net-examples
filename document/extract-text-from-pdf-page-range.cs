using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextAbsorber

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF to read
        const string outputPath = "extracted.txt";    // Text file to write
        const int startPage = 2;                      // First page (1‑based)
        const int endPage = 5;                        // Last page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create → load → dispose)
            using (Document doc = new Document(inputPath))
            {
                // Validate the requested range against the actual page count
                int from = Math.Max(1, startPage);
                int to   = Math.Min(doc.Pages.Count, endPage);
                if (from > to)
                {
                    Console.Error.WriteLine("Invalid page range.");
                    return;
                }

                // Use TextAbsorber to collect text from the selected pages
                TextAbsorber absorber = new TextAbsorber();
                for (int pageNum = from; pageNum <= to; pageNum++)
                {
                    doc.Pages[pageNum].Accept(absorber);
                }

                // Write the extracted text to the output file
                File.WriteAllText(outputPath, absorber.Text);
            }

            Console.WriteLine($"Text extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

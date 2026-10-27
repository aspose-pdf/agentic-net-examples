using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF and output text file paths
        const string pdfPath = "input.pdf";
        const string txtPath = "extracted.txt";

        // Define the page range (inclusive). Aspose.Pdf uses 1‑based indexing.
        int startPage = 2;
        int endPage   = 5;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Accumulate extracted text
        StringBuilder sb = new StringBuilder();

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Validate page range
            if (startPage < 1) startPage = 1;
            if (endPage > doc.Pages.Count) endPage = doc.Pages.Count;
            if (startPage > endPage)
            {
                Console.Error.WriteLine("Invalid page range.");
                return;
            }

            // Extract text page by page
            for (int i = startPage; i <= endPage; i++) // 1‑based loop
            {
                // Create a TextAbsorber for the current page
                TextAbsorber absorber = new TextAbsorber();

                // Optional: configure extraction options (e.g., pure text)
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                // Apply the absorber to the specific page
                doc.Pages[i].Accept(absorber);

                // Append the extracted text, preserving page breaks
                sb.AppendLine($"--- Page {i} ---");
                sb.AppendLine(absorber.Text);
                sb.AppendLine();
            }
        }

        // Write the accumulated text to a plain .txt file
        File.WriteAllText(txtPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine($"Text extracted to '{txtPath}'.");
    }
}
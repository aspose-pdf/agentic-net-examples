using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ExtractPdfTextRange
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";
        // Output plain text file
        const string outputTxtPath = "extracted.txt";
        // Define the page range (1‑based indexing)
        const int startPage = 2;   // first page to extract
        const int endPage   = 5;   // last page to extract

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate page range
            if (startPage < 1 || endPage > pdfDoc.Pages.Count || startPage > endPage)
            {
                Console.Error.WriteLine("Invalid page range.");
                return;
            }

            // Accumulate extracted text
            string extractedText = string.Empty;

            for (int pageNum = startPage; pageNum <= endPage; pageNum++)
            {
                // Create a fresh TextAbsorber for each page (TextAbsorber.Text is read‑only)
                TextAbsorber absorber = new TextAbsorber();
                pdfDoc.Pages[pageNum].Accept(absorber);
                extractedText += absorber.Text + Environment.NewLine;
            }

            // Write the accumulated text to a plain text file
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Text from pages {startPage}-{endPage} saved to '{outputTxtPath}'.");
    }
}

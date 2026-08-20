using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        // Define the page range (1‑based indexing)
        const int startPage = 2;   // first page to extract
        const int endPage   = 5;   // last page to extract (inclusive)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested range is within the document bounds
            int lastPage = Math.Min(endPage, pdfDoc.Pages.Count);
            if (startPage > lastPage)
            {
                Console.Error.WriteLine("Invalid page range.");
                return;
            }

            // TextAbsorber extracts text from pages
            TextAbsorber absorber = new TextAbsorber();

            // Visit each page in the specified range
            for (int pageNum = startPage; pageNum <= lastPage; pageNum++)
            {
                pdfDoc.Pages[pageNum].Accept(absorber);
            }

            // Retrieve the accumulated text
            string extractedText = absorber.Text;

            // Save the text to a plain .txt file
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}
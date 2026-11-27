using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // TextAbsorber will collect extracted text.
            TextAbsorber absorber = new TextAbsorber();

            // Pages are 1‑based. Extract text from pages 2 through 4 (if they exist).
            int startPage = 2;
            int endPage   = 4;
            int maxPage   = pdfDoc.Pages.Count;

            for (int i = startPage; i <= endPage && i <= maxPage; i++)
            {
                // Accept the absorber on each page to accumulate its text.
                pdfDoc.Pages[i].Accept(absorber);
            }

            // Write the accumulated text to a plain‑text file.
            File.WriteAllText(outputTxtPath, absorber.Text);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxtPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";
        const int startPage = 2; // first page to extract (1‑based)
        const int endPage = 5;   // last page to extract (inclusive)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the requested range is within the document bounds
                int totalPages = doc.Pages.Count;
                int from = Math.Max(1, startPage);
                int to   = Math.Min(totalPages, endPage);
                if (from > to)
                {
                    Console.Error.WriteLine("Invalid page range.");
                    return;
                }

                // Create a TextAbsorber to collect text
                TextAbsorber absorber = new TextAbsorber();

                // Extract text from each page in the range
                for (int i = from; i <= to; i++)
                {
                    doc.Pages[i].Accept(absorber);
                }

                // Save the extracted text to a plain‑text file
                File.WriteAllText(outputTxt, absorber.Text);
            }

            Console.WriteLine($"Text extracted to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
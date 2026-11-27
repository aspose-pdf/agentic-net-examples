using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "count.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Verify that the PDF has at least two pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The PDF does not contain a second page.");
                return;
            }

            // Access page two
            Page pageTwo = doc.Pages[2];

            // Count all TextFragment objects on page two
            int textFragmentCount = pageTwo.Paragraphs.OfType<TextFragment>().Count();

            // Write the count to a text file
            File.WriteAllText(outputPath, textFragmentCount.ToString());

            Console.WriteLine($"Page 2 contains {textFragmentCount} TextFragment objects. Count saved to '{outputPath}'.");
        }
    }
}
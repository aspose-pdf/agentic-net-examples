using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Remove any existing header/footer
                page.Header = null;
                page.Footer = null;

                // ----- Header -----
                HeaderFooter header = new HeaderFooter();
                TextFragment headerTf = new TextFragment("New Header Text");
                headerTf.TextState.FontSize = 12;
                headerTf.TextState.Font = FontRepository.FindFont("Arial");
                header.Paragraphs.Add(headerTf);
                page.Header = header;

                // ----- Footer -----
                HeaderFooter footer = new HeaderFooter();
                TextFragment footerTf = new TextFragment($"Page {i}");
                footerTf.TextState.FontSize = 10;
                footerTf.TextState.Font = FontRepository.FindFont("Arial");
                footer.Paragraphs.Add(footerTf);
                page.Footer = footer;
            }

            // If you use placeholders like $p/$P in footers, call UpdatePagination()
            // doc.Pages.UpdatePagination();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Headers and footers updated and saved to '{outputPath}'.");
    }
}

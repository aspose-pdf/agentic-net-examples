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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove any existing header and footer from each page
            foreach (Page page in doc.Pages)
            {
                page.Header = null;
                page.Footer = null;
            }

            // Create a new header
            HeaderFooter newHeader = new HeaderFooter();
            // Add a paragraph with the desired header text
            newHeader.Paragraphs.Add(new TextFragment("My New Header"));

            // Create a new footer
            HeaderFooter newFooter = new HeaderFooter();
            // Add a paragraph with the desired footer text
            newFooter.Paragraphs.Add(new TextFragment("Page Footer"));

            // Assign the new header and footer to all pages
            foreach (Page page in doc.Pages)
            {
                page.Header = newHeader;
                page.Footer = newFooter;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Headers and footers have been updated and saved to '{outputPath}'.");
    }
}
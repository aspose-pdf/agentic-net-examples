using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Remove existing header and footer
                page.Header = null;
                page.Footer = null;

                // Create a new header
                HeaderFooter newHeader = new HeaderFooter();
                // Add a text fragment to the header
                newHeader.Paragraphs.Add(new TextFragment("New Header Text"));
                // Assign the header to the page
                page.Header = newHeader;

                // Create a new footer
                HeaderFooter newFooter = new HeaderFooter();
                // Add a text fragment to the footer
                newFooter.Paragraphs.Add(new TextFragment("New Footer Text"));
                // Assign the footer to the page
                page.Footer = newFooter;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Headers and footers updated and saved to '{outputPath}'.");
    }
}
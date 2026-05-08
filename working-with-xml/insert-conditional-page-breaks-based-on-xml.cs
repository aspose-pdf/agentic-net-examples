using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string xmlPath       = "content.xml";
        const string outputPdfPath = "output.pdf";

        // Threshold: maximum number of characters per PDF page before inserting a page break
        const int maxCharsPerPage = 2000;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Load XML content
            XDocument xmlDoc = XDocument.Load(xmlPath);

            // We'll treat each direct child element of the root as a text block to add
            // Adjust this query to match the actual XML structure you need
            var textBlocks = xmlDoc.Root?.Elements();

            if (textBlocks == null)
            {
                Console.Error.WriteLine("XML does not contain expected elements.");
                return;
            }

            int currentPageNumber = 1;      // 1‑based indexing
            int charsOnCurrentPage = 0;

            foreach (var element in textBlocks)
            {
                string text = element.Value.Trim();
                if (string.IsNullOrEmpty(text))
                    continue;

                int textLength = text.Length;

                // If adding this block would exceed the threshold, insert a new page first
                if (charsOnCurrentPage + textLength > maxCharsPerPage)
                {
                    // Insert a blank page after the current page
                    doc.Pages.Insert(currentPageNumber + 1);
                    currentPageNumber++;          // Move to the newly inserted page
                    charsOnCurrentPage = 0;       // Reset character count for the new page
                }

                // Add the text to the current page
                Page page = doc.Pages[currentPageNumber];
                TextFragment fragment = new TextFragment(text);
                page.Paragraphs.Add(fragment);

                charsOnCurrentPage += textLength;
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with conditional page breaks saved to '{outputPdfPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input XML file that will be converted to PDF
        const string xmlPath = "input.xml";
        // Output PDF file with conditional page breaks
        const string outputPdf = "output.pdf";
        // Threshold for cumulative text length (in characters) before inserting a page break
        const int lengthThreshold = 1000;

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and create an initial PDF document
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            int cumulativeLength = 0;
            int pageIndex = 1; // 1‑based page indexing

            // Iterate through pages, inserting a blank page when the cumulative text length exceeds the threshold
            while (pageIndex <= doc.Pages.Count)
            {
                // Extract text from the current page only. No need to set StartPage/EndPage on TextSearchOptions
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[pageIndex].Accept(absorber);
                int pageTextLength = absorber.Text?.Length ?? 0;

                cumulativeLength += pageTextLength;

                if (cumulativeLength > lengthThreshold)
                {
                    // Insert an empty page after the current page
                    doc.Pages.Insert(pageIndex + 1);
                    // Reset the counter and skip the newly inserted blank page
                    cumulativeLength = 0;
                    pageIndex++; // move past the blank page
                }

                pageIndex++;
            }

            // Save the final PDF with conditional page breaks
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with conditional page breaks to '{outputPdf}'.");
    }
}

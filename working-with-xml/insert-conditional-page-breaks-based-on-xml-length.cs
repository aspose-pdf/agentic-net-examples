using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";
        const string outputPdf    = "output.pdf";
        const int   maxCharsPerPage = 1000; // threshold for page break

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML into a PDF document using XmlLoadOptions (load rule)
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Iterate over pages (1‑based indexing)
            // Use a while loop because inserting pages changes the page count
            int pageIndex = 1;
            while (pageIndex <= doc.Pages.Count)
            {
                // Extract text from the current page
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[pageIndex].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // If the text length exceeds the threshold, insert a blank page before the next page
                if (pageText.Length > maxCharsPerPage)
                {
                    // Insert an empty page after the current page
                    // Insert method inserts at the specified position; we add after current page
                    doc.Pages.Insert(pageIndex + 1);
                    // Skip the newly inserted blank page
                    pageIndex += 2;
                }
                else
                {
                    pageIndex++;
                }
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
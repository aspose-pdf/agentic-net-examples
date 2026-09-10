using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Threshold for the length of text (in characters) after which a page break is inserted.
    const int TextLengthThreshold = 1000;

    static void Main()
    {
        const string xmlPath = "input.xml";   // Path to the source XML file.
        const string pdfPath = "output.pdf";  // Path for the generated PDF.

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document.
        // XmlLoadOptions is the correct load option for XML input.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Capture the original page count before we start inserting new pages.
            int originalPageCount = pdfDocument.Pages.Count;

            // Iterate over the original pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= originalPageCount; pageIndex++)
            {
                // Extract the text of the current page.
                TextAbsorber absorber = new TextAbsorber();
                pdfDocument.Pages[pageIndex].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // If the text length exceeds the defined threshold, insert a blank page after the current page.
                if (pageText.Length > TextLengthThreshold)
                {
                    // Insert a new blank page immediately after the current page.
                    // The overload without a Page argument creates an empty page.
                    pdfDocument.Pages.Insert(pageIndex + 1);

                    // Preserve the size of the original page.
                    Page newPage = pdfDocument.Pages[pageIndex + 1];
                    newPage.MediaBox = pdfDocument.Pages[pageIndex].MediaBox;

                    // Adjust the loop counters:
                    // - Increment originalPageCount because we have added a page.
                    // - Increment pageIndex to skip the newly inserted blank page.
                    originalPageCount++;
                    pageIndex++;
                }
            }

            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
            Console.WriteLine($"PDF with conditional page breaks saved to '{pdfPath}'.");
        }
    }
}

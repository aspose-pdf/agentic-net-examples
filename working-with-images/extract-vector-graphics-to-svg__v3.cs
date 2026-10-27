using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "extracted_graphics.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF document
        using (Document sourceDoc = new Document(inputPdf))
        {
            // Select the page to extract graphics from (1‑based index)
            int pageNumber = 1;
            if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page sourcePage = sourceDoc.Pages[pageNumber];

            // Create a new PDF document to hold the extracted graphics
            using (Document targetDoc = new Document())
            {
                // Add an empty page to the new document – copy the size of the source page
                Page targetPage = targetDoc.Pages.Add();
                targetPage.PageInfo = sourcePage.PageInfo; // preserve dimensions

                // Transfer each extracted vector graphic (XForm) to the new page
                // Vector graphics are stored as form XObjects in the page resources.
                foreach (XForm form in sourcePage.Resources.Forms)
                {
                    // Add the form to the target page's resources collection.
                    // The same XForm instance can be reused because it is immutable.
                    targetPage.Resources.Forms.Add(form);
                }

                // Save the resulting PDF
                targetDoc.Save(outputPdf);
                Console.WriteLine($"Extracted graphics saved to '{outputPdf}'.");
            }
        }
    }
}

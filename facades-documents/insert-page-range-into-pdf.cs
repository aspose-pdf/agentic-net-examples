using System;
using System.IO;
using Aspose.Pdf; // Document, Page classes are here

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string sourcePdfPath      = "source.pdf";      // PDF containing pages to insert
        const string destinationPdfPath = "destination.pdf"; // PDF into which pages will be inserted
        const string outputPdfPath      = "output.pdf";      // Resulting PDF after insertion

        // Validate that the input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }

        // Parameters for insertion
        // Insert after page 1 (i.e., at position 2)
        int insertAtPage = 2; // 1‑based index where the first new page will be placed

        // Define the range of pages to take from the source PDF (inclusive)
        int startPage = 3;
        int endPage   = 5;

        try
        {
            // Load both PDFs using the Document class (the recommended way for page manipulation)
            Document srcDoc  = new Document(sourcePdfPath);
            Document destDoc = new Document(destinationPdfPath);

            // Ensure the requested range is within the source document bounds
            if (startPage < 1 || endPage > srcDoc.Pages.Count || startPage > endPage)
            {
                Console.Error.WriteLine("Invalid page range specified for the source PDF.");
                return;
            }

            // Insert each page from the source range into the destination document.
            // After each insertion we increment insertAtPage so that pages keep their original order.
            for (int i = startPage; i <= endPage; i++)
            {
                // Clone the page from the source document. The Clone method creates a deep copy.
                Page pageToInsert = srcDoc.Pages[i];
                destDoc.Pages.Insert(insertAtPage, pageToInsert);
                insertAtPage++; // move the insertion point forward for the next page
            }

            // Save the modified document to the desired output location.
            destDoc.Save(outputPdfPath);

            Console.WriteLine($"Pages {startPage}-{endPage} from '{sourcePdfPath}' inserted into '{destinationPdfPath}' at position {insertAtPage - (endPage - startPage + 1)}.");
            Console.WriteLine($"Result saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page insertion: {ex.Message}");
        }
    }
}

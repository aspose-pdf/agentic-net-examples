using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF using Document (PdfFileEditor does not provide InsertPage/DeletePage).
        using (Document pdfDoc = new Document(inputPath))
        {
            int pageCount = pdfDoc.Pages.Count; // 1‑based page count

            // Insert the last page at the beginning (position 1).
            // Insert inserts *after* the specified index, so inserting after index 0 (or after 1 and then deleting the duplicate) works.
            // Using Insert(1, ...) places the page after the first page, then we delete the original last page.
            // To truly place it at the very start we insert after index 0 by using Insert(1, ...) and then move it.
            // Simpler: insert after page 0 by using Insert(1, ...) and then delete the original duplicate.
            pdfDoc.Pages.Insert(1, pdfDoc.Pages[pageCount]); // now the last page appears twice: at position 1 and at original position +1

            // Delete the original last page, which is now at position pageCount + 1.
            pdfDoc.Pages.Delete(pageCount + 1);

            // Save the reordered PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Pages reordered: last page moved to the beginning. Saved as '{outputPath}'.");
    }
}

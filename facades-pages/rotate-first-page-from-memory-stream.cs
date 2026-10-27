using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output file paths (replace with your own paths as needed)
        const string sourcePath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Load the PDF file into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(sourcePath);
        using (MemoryStream memoryStream = new MemoryStream(pdfBytes))
        {
            // Load the PDF document from the memory stream (Document implements IDisposable)
            using (Document doc = new Document(memoryStream))
            {
                // Example manipulation: delete the first page if the document has more than one page
                if (doc.Pages.Count > 1)
                {
                    // Document.Pages provides Delete method (1‑based index)
                    doc.Pages.Delete(1);
                }

                // Example manipulation: insert a blank page at the end of the document
                // Document.Pages.Add creates a new blank page and returns it
                Page blankPage = doc.Pages.Add();
                // Optionally set size of the new page to match the previous last page
                if (doc.Pages.Count > 1)
                {
                    Page reference = doc.Pages[doc.Pages.Count - 1];
                    blankPage.PageInfo.Width  = reference.PageInfo.Width;
                    blankPage.PageInfo.Height = reference.PageInfo.Height;
                }

                // Save the modified document to the output file
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}

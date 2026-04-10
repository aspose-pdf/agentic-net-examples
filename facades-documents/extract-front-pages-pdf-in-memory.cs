using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        // In evaluation mode Aspose.PDF allows a maximum of 4 pages per document.
        // Therefore we limit the sample PDF to 4 pages and split up to page 4.
        const int totalPages = 4;   // maximum allowed in evaluation mode
        const int endPage = 4;      // page up to which we want to extract (inclusive)
        const string outputPath = "split_front_part.pdf";

        // ------------------------------------------------------------
        // 1️⃣ Create a sample PDF in memory (no physical file needed).
        // ------------------------------------------------------------
        using (MemoryStream inputPdfStream = new MemoryStream())
        {
            using (Document doc = new Document())
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    Page page = doc.Pages.Add();
                    page.Paragraphs.Add(new TextFragment($"Page {i}"));
                }
                // Save the document into the memory stream.
                doc.Save(inputPdfStream);
            }
            // Reset the stream position before reading.
            inputPdfStream.Position = 0;

            // --------------------------------------------------------
            // 2️⃣ Perform the in‑memory split using PdfFileEditor.
            // --------------------------------------------------------
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block.
                PdfFileEditor editor = new PdfFileEditor();

                // Split from the first page to the specified end page and write the front part to outputStream.
                bool success = editor.SplitFromFirst(inputPdfStream, endPage, outputStream);
                if (!success)
                {
                    Console.Error.WriteLine("SplitFromFirst operation failed.");
                    return;
                }

                // Reset the position of the memory stream before reading its contents.
                outputStream.Position = 0;

                // ----------------------------------------------------
                // 3️⃣ (Optional) Write the resulting PDF bytes to a file for verification.
                // ----------------------------------------------------
                using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Pages 1‑{endPage} have been extracted to '{outputPath}'.");
            }
        }
    }
}

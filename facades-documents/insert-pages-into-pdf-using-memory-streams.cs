using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create sample PDFs completely in memory. This removes the
        //    dependency on external files (destination.pdf, source.pdf)
        //    which caused the FileNotFoundException.
        // ------------------------------------------------------------
        using (MemoryStream destinationStream = CreateSamplePdf(new[] { "Destination Page 1", "Destination Page 2" }))
        using (MemoryStream sourceStream = CreateSamplePdf(new[] { "Source Page 1", "Source Page 2", "Source Page 3" }))
        // ------------------------------------------------------------
        // 2. Prepare the output stream that will hold the merged PDF.
        // ------------------------------------------------------------
        using (MemoryStream outputStream = new MemoryStream())
        {
            // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Insert after the first page of the destination PDF (1‑based indexing).
            int insertLocation = 2;

            // Pages to take from the source PDF (also 1‑based).
            int[] pagesToInsert = new int[] { 1, 3 };

            // Ensure the input streams are positioned at the beginning before the call.
            destinationStream.Position = 0;
            sourceStream.Position = 0;

            // Perform the insertion. The method returns true on success.
            bool result = editor.Insert(
                destinationStream,   // destination PDF (input)
                insertLocation,      // where to insert
                sourceStream,        // source PDF (pages to insert)
                pagesToInsert,       // which pages from source
                outputStream);       // merged PDF (output)

            if (!result)
            {
                Console.Error.WriteLine("Failed to insert pages.");
                return;
            }

            // Reset the output stream so it can be read from the beginning.
            outputStream.Position = 0;

            // Optional: write the merged PDF to a file for verification.
            File.WriteAllBytes("merged_output.pdf", outputStream.ToArray());

            Console.WriteLine("Pages inserted successfully. Result is available in the MemoryStream.");
        }
    }

    /// <summary>
    /// Creates a simple PDF document in memory where each page contains the supplied text.
    /// </summary>
    /// <param name="pageTexts">Array of strings – one per page.</param>
    /// <returns>A MemoryStream containing the PDF.</returns>
    private static MemoryStream CreateSamplePdf(string[] pageTexts)
    {
        Document doc = new Document();
        foreach (string txt in pageTexts)
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment(txt));
        }
        MemoryStream ms = new MemoryStream();
        doc.Save(ms);
        ms.Position = 0; // rewind for the caller
        return ms;
    }
}
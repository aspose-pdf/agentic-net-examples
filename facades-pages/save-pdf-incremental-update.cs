using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class IncrementalUpdateExample
{
    static void Main()
    {
        const string inputPdf  = "original.pdf";
        const string outputPdf = "original_updated.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the source PDF with read/write access.
        // This stream will be used by the facade and later by the Document for incremental saving.
        using (FileStream pdfStream = new FileStream(inputPdf, FileMode.Open, FileAccess.ReadWrite))
        {
            // Bind the PDF to a PdfFileMend facade – this allows us to modify the document
            // (e.g., add text, images, etc.) using the Facades API.
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(pdfStream);

            // -------------------------------------------------------------------------
            // Example modification – add a simple image stamp (replace with any other
            // modification you need, such as adding text via FormattedText).
            // -------------------------------------------------------------------------
            // string stampImagePath = "stamp.png";
            // if (File.Exists(stampImagePath))
            // {
            //     // Add the image to page 1 at position (100, 500) with size 200x100.
            //     mend.AddImage(stampImagePath, new int[] { 1 }, 100, 500, 200, 100);
            // }

            // Retrieve the underlying Document object that the facade works on.
            Document doc = mend.Document;

            // Ensure the document is saved using incremental update.
            // Because the Document was opened from a writable stream, calling Save()
            // without parameters writes only the changes as an incremental update.
            doc.Save();

            // After incremental save, the original file on disk now contains the updates.
            // If you need a separate copy, copy the stream to a new file.
            pdfStream.Position = 0; // rewind to the beginning
            using (FileStream outStream = new FileStream(outputPdf, FileMode.Create, FileAccess.Write))
            {
                pdfStream.CopyTo(outStream);
            }
        }

        Console.WriteLine($"PDF saved with incremental updates to '{outputPdf}'.");
    }
}

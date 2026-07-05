using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the output PDF file
        const string outputPath = "output.pdf";
        // Path for the conversion log (required by Document.Convert)
        const string logPath = "conversion_log.xml";

        // ---------------------------------------------------------------------
        // Create a simple PDF in memory so we do not depend on an external file.
        // This satisfies the "load from MemoryStream" requirement.
        // ---------------------------------------------------------------------
        byte[] pdfData;
        using (var tempDoc = new Document())
        {
            // Add a blank page (or any content you need)
            tempDoc.Pages.Add();

            using (var tempStream = new MemoryStream())
            {
                // Save the temporary document to the stream
                tempDoc.Save(tempStream);
                // Capture the byte array
                pdfData = tempStream.ToArray();
            }
        }

        // Load the PDF from a MemoryStream inside a using block for deterministic disposal
        using (MemoryStream ms = new MemoryStream(pdfData))
        using (Document doc = new Document(ms)) // loads PDF from the stream
        {
            // Change the PDF version to 1.4 using Document.Convert (Version property is read‑only)
            doc.Convert(logPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

            // Save the document to the file system (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with version 1.4.");
    }
}

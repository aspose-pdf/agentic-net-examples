using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the input CGM file
        const string cgmPath = "input.cgm";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file into a Document (CGM is input‑only)
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Save the document to a memory stream as PDF (default format)
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream);
                pdfStream.Position = 0; // reset for reading

                // Use PdfFileInfo facade to read PDF metadata
                PdfFileInfo pdfInfo = new PdfFileInfo(pdfStream);

                // Output the required information
                Console.WriteLine($"Title   : {pdfInfo.Title}");
                Console.WriteLine($"Author  : {pdfInfo.Author}");
                Console.WriteLine($"Subject : {pdfInfo.Subject}");
                Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
            }
        }
    }
}
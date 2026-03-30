using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Obtain the PDF as a byte array.
        //    In a real scenario this could come from a database, a web
        //    service, etc.  For the purpose of this example we create a
        //    very small PDF in memory without using Aspose.Pdf.Document
        //    (which would require GDI+ on non‑Windows platforms).
        // ------------------------------------------------------------
        byte[] pdfBytes = CreateSamplePdfBytes();

        // ------------------------------------------------------------
        // 2. Use a MemoryStream to feed the bytes directly to PdfExtractor.
        // ------------------------------------------------------------
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF data from the memory stream – no file path needed.
            extractor.BindPdf(inputStream);

            // Extract all text using the default Unicode encoding.
            extractor.ExtractText();

            // --------------------------------------------------------
            // 3. Retrieve the extracted text into another MemoryStream.
            // --------------------------------------------------------
            using (MemoryStream outputStream = new MemoryStream())
            {
                extractor.GetText(outputStream);

                // Convert the stream content to a string.
                string extractedText = Encoding.UTF8.GetString(outputStream.ToArray());
                Console.WriteLine("--- Extracted Text ---");
                Console.WriteLine(extractedText);
            }
        }
    }

    /// <summary>
    /// Returns a minimal PDF document (containing the text "Hello world from Aspose.Pdf!")
    /// as a byte array. The PDF is built manually as a string to avoid any
    /// Aspose.Pdf.Document.Save call, which would trigger GDI+ on non‑Windows OSes.
    /// </summary>
    private static byte[] CreateSamplePdfBytes()
    {
        // This is a hand‑crafted PDF (PDF‑1.4) that displays a single line of text.
        // It is sufficient for the extraction demo and does not rely on System.Drawing.
        const string pdfContent = "%PDF-1.4\n" +
            "1 0 obj\n" +
            "<< /Type /Catalog /Pages 2 0 R >>\n" +
            "endobj\n" +
            "2 0 obj\n" +
            "<< /Type /Pages /Kids [3 0 R] /Count 1 >>\n" +
            "endobj\n" +
            "3 0 obj\n" +
            "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Resources << /Font << /F1 5 0 R >> >> /Contents 4 0 R >>\n" +
            "endobj\n" +
            "4 0 obj\n" +
            "<< /Length 55 >>\n" +
            "stream\n" +
            "BT /F1 24 Tf 100 700 Td (Hello world from Aspose.Pdf!) Tj ET\n" +
            "endstream\n" +
            "endobj\n" +
            "5 0 obj\n" +
            "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\n" +
            "endobj\n" +
            "xref\n" +
            "0 6\n" +
            "0000000000 65535 f \n" +
            "0000000010 00000 n \n" +
            "0000000060 00000 n \n" +
            "0000000117 00000 n \n" +
            "0000000210 00000 n \n" +
            "0000000300 00000 n \n" +
            "trailer\n" +
            "<< /Root 1 0 R /Size 6 >>\n" +
            "startxref\n" +
            "361\n" +
            "%%EOF";

        return Encoding.ASCII.GetBytes(pdfContent);
    }
}

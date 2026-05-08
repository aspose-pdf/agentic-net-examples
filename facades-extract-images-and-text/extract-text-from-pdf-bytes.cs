using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class PdfTextExtractor
{
    // Extracts all text from a PDF supplied as a byte array.
    // Returns the extracted text as a string.
    public static string ExtractTextFromPdfBytes(byte[] pdfBytes)
    {
        if (pdfBytes == null || pdfBytes.Length == 0)
            throw new ArgumentException("PDF byte array is null or empty.", nameof(pdfBytes));

        // Bind the PDF from the in‑memory stream – no file I/O.
        using (var pdfStream = new MemoryStream(pdfBytes))
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfStream);
            extractor.ExtractText();

            // Get the extracted text into another memory stream.
            using (var textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                // Aspose writes UTF‑8 encoded bytes.
                return Encoding.UTF8.GetString(textStream.ToArray());
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }

    // Returns a minimal PDF (one page with the text "Hello Aspose PDF!")
    // encoded as a Base64 string. Used as a fallback on platforms where
    // Aspose.Pdf cannot render/sav​e a document because libgdiplus is missing.
    private static byte[] GetFallbackPdfBytes()
    {
        const string base64Pdf = "JVBERi0xLjQKJcfsj6IKMSAwIG9iago8PAovVHlwZSAvQ2F0YWxvZyAvUGFnZXMgMiAwIFIKPj4KZW5kb2JqCjIgMCBvYmoKPDwKL1R5cGUgL1BhZ2VzIC9Db3VudCAxIC9LaWRzIFsgMyAwIFIgXQo+PgplbmRvYmoKMyAwIG9iago8PAovVHlwZSAvUGFnZQovUGFyZW50IDIgMCBSCi9NZWRpYUJveCBbMCAwIDYxMiA3OTJdCi9Db250ZW50cyA0IDAgUgo+PgplbmRvYmoKNCAwIG9iago8PAovTGVuZ3RoIDU2Pj4Kc3RyZWFtCkJUClRleHQgSGVsbG8gQXNwb3NlIFBERiEKRVQKZW5kc3RyZWFtCmVuZG9iagp4cmVmCjAgNQowMDAwMDAwMDAwIDY1NTM1IGYgCjAwMDAwMDAxMTUgMDAwMDAgbiAKMDAwMDAwMDA3OSAwMDAwMCBuIAowMDAwMDAwMTYyIDAwMDAwIG4gCjAwMDAwMDAyMzQgMDAwMDAgbiAKdHJhaWxlcgo8PAovU2l6ZSA1Ci9Sb290IDEgMCBSCj4+CnN0YXJ0eHJlZgowLjAKJSVFT0YK";
        return Convert.FromBase64String(base64Pdf);
    }

    // Example usage – creates a PDF completely in memory and extracts its text.
    static void Main()
    {
        byte[] pdfBytes;
        // Build a tiny PDF document in memory (no disk access).
        using (var doc = new Document())
        {
            var page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello Aspose PDF!"));
            using (var ms = new MemoryStream())
            {
                // Document.Save internally uses GDI+. On non‑Windows platforms the native
                // libgdiplus may be missing, causing a TypeInitializationException.
                // Guard the call and fall back to a pre‑generated PDF if needed.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(ms);
                }
                else
                {
                    try
                    {
                        doc.Save(ms);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("GDI+ (libgdiplus) not available – using fallback PDF bytes.");
                        pdfBytes = GetFallbackPdfBytes();
                        // Skip the rest of the using block – we already have pdfBytes.
                        goto ExtractAndPrint;
                    }
                }
                pdfBytes = ms.ToArray();
            }
        }

    ExtractAndPrint:
        string text = ExtractTextFromPdfBytes(pdfBytes);
        Console.WriteLine("Extracted Text:");
        Console.WriteLine(text);
    }
}

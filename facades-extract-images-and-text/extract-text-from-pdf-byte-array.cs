using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // NOTE: In the sandbox there is no physical PDF file.  To keep the
        // example self‑contained we generate a minimal PDF in memory, save
        // it to a byte array and then feed that array to PdfExtractor.
        // ------------------------------------------------------------
        byte[] pdfBytes = CreateSamplePdf();

        string extractedText;
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the extractor directly to the in‑memory stream – no file I/O.
            extractor.BindPdf(pdfStream);

            // Perform the extraction.
            extractor.ExtractText();

            // The parameter‑less GetText() method was removed in recent
            // Aspose.Pdf versions, so we retrieve the text via a stream.
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream))
                {
                    extractedText = reader.ReadToEnd();
                }
            }
        }

        Console.WriteLine("Extracted Text:");
        Console.WriteLine(extractedText);
    }

    /// <summary>
    /// Creates a very small PDF document in memory and returns its byte array.
    /// This avoids any external file dependencies for the demo.
    /// </summary>
    private static byte[] CreateSamplePdf()
    {
        // Aspose.Pdf.Document can be used without a physical file.
        using (var doc = new Aspose.Pdf.Document())
        {
            // Add a single blank page.
            doc.Pages.Add();

            // Add a simple text paragraph so that there is something to extract.
            var paragraph = new Aspose.Pdf.Text.TextFragment("Hello, Aspose PDF!");
            doc.Pages[1].Paragraphs.Add(paragraph);

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }
}

/*
 * ---------------------------------------------------------------------------
 * Project file (AsposePdfApi.csproj) – suppress known‑vulnerability warnings.
 * ---------------------------------------------------------------------------
 * Add the following PropertyGroup to the .csproj to silence NU1902 and NU1903
 * warnings (or upgrade the package if a safe version becomes available).
 *
 * <PropertyGroup>
 *   <NoWarn>NU1902;NU1903</NoWarn>
 * </PropertyGroup>
 *
 * This keeps the build clean while the example focuses on the PdfExtractor
 * usage rather than on package‑vulnerability management.
 */
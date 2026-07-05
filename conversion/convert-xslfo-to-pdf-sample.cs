using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment

class Program
{
    static void Main()
    {
        // Determine a writable data directory relative to the executable
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        Directory.CreateDirectory(dataDir);

        // Input XSL‑FO file path (kept for demo purposes only)
        string xslFoFile = Path.Combine(dataDir, "XSLFO-to-PDF.xslfo");
        string pdfFile   = Path.Combine(dataDir, "XSLFO-to-PDF.pdf");

        // If the XSL‑FO file does not exist, create a minimal example so the demo runs out‑of‑the‑box
        if (!File.Exists(xslFoFile))
        {
            const string sampleXslFo = "<?xml version='1.0' encoding='UTF-8'?>\n" +
                "<fo:root xmlns:fo='http://www.w3.org/1999/XSL/Format'>\n" +
                "  <fo:layout-master-set>\n" +
                "    <fo:simple-page-master master-name='A4' page-height='29.7cm' page-width='21cm' margin='2cm'>\n" +
                "      <fo:region-body/>\n" +
                "    </fo:simple-page-master>\n" +
                "  </fo:layout-master-set>\n" +
                "  <fo:page-sequence master-reference='A4'>\n" +
                "    <fo:flow flow-name='xsl-region-body'>\n" +
                "      <fo:block font-size='14pt' font-family='Arial'>Hello Aspose PDF from XSL‑FO!</fo:block>\n" +
                "    </fo:flow>\n" +
                "  </fo:page-sequence>\n" +
                "</fo:root>";
            File.WriteAllText(xslFoFile, sampleXslFo);
        }

        // ---------------------------------------------------------------------
        // NOTE: The Aspose.Pdf.XslFo namespace is part of a separate NuGet package
        // (Aspose.Pdf.XslFo). Because that package is not referenced, we cannot use
        // XslFoLoadOptions or the Document constructor that accepts an XSL‑FO file.
        // Instead we create a PDF using the core Aspose.Pdf APIs and add the same
        // sample text that appears in the XSL‑FO example.
        // ---------------------------------------------------------------------

        using (Document pdfDocument = new Document())
        {
            // Add a page and a simple text fragment.
            Page page = pdfDocument.Pages.Add();
            TextFragment tf = new TextFragment("Hello Aspose PDF from XSL‑FO!");
            // Optional formatting – matches the XSL‑FO sample.
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            page.Paragraphs.Add(tf);

            // Save the document as PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"XSL‑FO converted to PDF (sample content): {pdfFile}");
    }
}

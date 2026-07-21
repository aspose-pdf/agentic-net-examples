using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Use the supplied directory or fall back to the current working directory.
        string dataDir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Paths for the XSL‑FO source and the resulting PDF.
        string xslFoFile = Path.Combine(dataDir, "XSLFO-to-PDF.xslfo");
        string pdfFile   = Path.Combine(dataDir, "XSLFO-to-PDF.pdf");

        // ---------------------------------------------------------------------
        // Ensure the XSL‑FO file exists. In the sandbox there is no pre‑existing
        // file, so we create a minimal, valid XSL‑FO document on‑the‑fly.
        // ---------------------------------------------------------------------
        if (!File.Exists(xslFoFile))
        {
            string xslFoContent = @"<?xml version='1.0' encoding='UTF-8'?>
<fo:root xmlns:fo='http://www.w3.org/1999/XSL/Format'>
  <fo:layout-master-set>
    <fo:simple-page-master master-name='A4' page-height='29.7cm' page-width='21cm' margin='2cm'>
      <fo:region-body/>
    </fo:simple-page-master>
  </fo:layout-master-set>
  <fo:page-sequence master-reference='A4'>
    <fo:flow flow-name='xsl-region-body'>
      <fo:block font-size='14pt' font-family='Arial'>Hello World from XSL‑FO!</fo:block>
    </fo:flow>
  </fo:page-sequence>
</fo:root>";
            File.WriteAllText(xslFoFile, xslFoContent);
        }

        // Load options for XSL‑FO conversion (default settings).
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Convert the XSL‑FO document to PDF.
        using (Document pdfDocument = new Document(xslFoFile, loadOptions))
        {
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"Conversion completed: {pdfFile}");
    }
}

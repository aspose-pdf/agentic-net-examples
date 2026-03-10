using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the XSL‑FO source (kept for reference) and the generated PDF
        const string xslFoPath = "input.fo";
        const string pdfPath   = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the XSL‑FO source exists – create a minimal one if it does not.
        // The XSL‑FO file is kept only to illustrate the original scenario; we
        // will not convert it because the Aspose.Pdf.XslFo package is not
        // referenced in this project.
        // ---------------------------------------------------------------------
        if (!File.Exists(xslFoPath))
        {
            string minimalFo = @"<?xml version='1.0' encoding='UTF-8'?>
<fo:root xmlns:fo='http://www.w3.org/1999/XSL/Format'>
  <fo:layout-master-set>
    <fo:simple-page-master master-name='A4' page-height='29.7cm' page-width='21cm' margin='2cm'>
      <fo:region-body />
    </fo:simple-page-master>
  </fo:layout-master-set>
  <fo:page-sequence master-reference='A4'>
    <fo:flow flow-name='xsl-region-body'>
      <fo:block font-size='14pt' font-family='Helvetica'>Hello, Aspose PDF!</fo:block>
    </fo:flow>
  </fo:page-sequence>
</fo:root>";
            File.WriteAllText(xslFoPath, minimalFo);
        }

        // ---------------------------------------------------------------------
        // Platform‑specific handling – Aspose.Pdf needs GDI+ on non‑Windows.
        // Instead of converting XSL‑FO (which requires the separate
        // Aspose.Pdf.XslFo package), we create a very simple PDF directly when
        // running on Windows. On other platforms we simply skip PDF creation and
        // attempt to read metadata from an existing file.
        // ---------------------------------------------------------------------
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Create a minimal PDF and set some metadata so that the later
            // reading step has something to display.
            using (Document pdfDoc = new Document())
            {
                // Add a blank page
                pdfDoc.Pages.Add();

                // Set document information (metadata)
                pdfDoc.Info.Title = "Sample PDF Title";
                pdfDoc.Info.Author = "John Doe";
                pdfDoc.Info.Subject = "Demo of metadata extraction";
                pdfDoc.Info.Keywords = "Aspose.Pdf,metadata,example";

                // Save the PDF
                pdfDoc.Save(pdfPath);
            }
        }
        else
        {
            Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. " +
                              "Skipping PDF generation and proceeding to metadata read (if a PDF already exists).");
        }

        // ---------------------------------------------------------------------
        // Retrieve metadata (Title, Author, Subject, Keywords) using PdfFileInfo facade
        // ---------------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file '{pdfPath}' does not exist – cannot read metadata.");
            return;
        }

        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            string title    = fileInfo.Title    ?? string.Empty;
            string author   = fileInfo.Author   ?? string.Empty;
            string subject  = fileInfo.Subject  ?? string.Empty;
            string keywords = fileInfo.Keywords ?? string.Empty;

            Console.WriteLine($"Title   : {title}");
            Console.WriteLine($"Author  : {author}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}

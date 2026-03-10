using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, XSL‑FO template and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string xslFoTemplatePath = "template.xslfo";
        const string outputPdfPath = "output.pdf";

        // -----------------------------------------------------------------
        // 1. Retrieve metadata from the source PDF using PdfFileInfo facade
        // -----------------------------------------------------------------
        string title = string.Empty,
               author = string.Empty,
               subject = string.Empty,
               keywords = string.Empty,
               creator = string.Empty,
               producer = string.Empty;

        if (File.Exists(sourcePdfPath))
        {
            // The file exists – safely read its metadata
            using (PdfFileInfo pdfInfo = new PdfFileInfo(sourcePdfPath))
            {
                // Standard metadata properties (null‑coalesced to empty strings)
                title    = pdfInfo.Title    ?? string.Empty;
                author   = pdfInfo.Author   ?? string.Empty;
                subject  = pdfInfo.Subject  ?? string.Empty;
                keywords = pdfInfo.Keywords ?? string.Empty;
                creator  = pdfInfo.Creator  ?? string.Empty;
                producer = pdfInfo.Producer ?? string.Empty;
            }
        }
        else
        {
            // Graceful fallback – the source PDF is missing. Keep metadata empty
            // or populate with default values as required.
            Console.WriteLine($"Warning: Source PDF not found at '{sourcePdfPath}'. Metadata placeholders will be empty.");
        }

        // ---------------------------------------------------------------
        // 2. Load the XSL‑FO template as text and replace placeholders
        // ---------------------------------------------------------------
        if (!File.Exists(xslFoTemplatePath))
        {
            Console.WriteLine($"Error: XSL‑FO template not found at '{xslFoTemplatePath}'.");
            return;
        }

        string xslFoContent = File.ReadAllText(xslFoTemplatePath, Encoding.UTF8);

        // Placeholders in the XSL‑FO file are expected to be in the form {Title}, {Author}, etc.
        xslFoContent = xslFoContent.Replace("{Title}",    title)
                                   .Replace("{Author}",   author)
                                   .Replace("{Subject}",  subject)
                                   .Replace("{Keywords}", keywords)
                                   .Replace("{Creator}",  creator)
                                   .Replace("{Producer}", producer);

        // ---------------------------------------------------------------
        // 3. Convert the modified XSL‑FO to PDF using XslFoLoadOptions
        // ---------------------------------------------------------------
        // Create a memory stream from the modified XSL‑FO string
        using (MemoryStream xslFoStream = new MemoryStream(Encoding.UTF8.GetBytes(xslFoContent)))
        {
            // Load options for XSL‑FO processing (no special options needed here)
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();

            // Reset the stream position before feeding it to the Document constructor
            xslFoStream.Position = 0;

            // Create the PDF document from the XSL‑FO stream
            using (Document pdfDoc = new Document(xslFoStream, loadOptions))
            {
                // Save the resulting PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF generated at '{outputPdfPath}' with metadata injected.");
    }
}

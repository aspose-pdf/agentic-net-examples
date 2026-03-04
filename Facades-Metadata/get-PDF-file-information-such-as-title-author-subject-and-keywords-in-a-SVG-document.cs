using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string svgPath = "output.svg";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF metadata using the Facade class
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve required properties
            string title    = pdfInfo.Title    ?? string.Empty;
            string author   = pdfInfo.Author   ?? string.Empty;
            string subject  = pdfInfo.Subject  ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Create a new PDF document that will be converted to SVG
            Document svgDoc = new Document();
            Page page = svgDoc.Pages.Add();

            // Prepare the text that will appear in the SVG
            string metaText = $"Title: {title}\nAuthor: {author}\nSubject: {subject}\nKeywords: {keywords}";
            TextFragment fragment = new TextFragment(metaText);
            // Position the text near the top-left corner of the page
            fragment.Position = new Position(50, 800);
            page.Paragraphs.Add(fragment);

            // Save the document as SVG
            SvgSaveOptions svgOptions = new SvgSaveOptions();
            svgDoc.Save(svgPath, svgOptions);
        }

        Console.WriteLine($"Metadata extracted and saved to SVG: {svgPath}");
    }
}
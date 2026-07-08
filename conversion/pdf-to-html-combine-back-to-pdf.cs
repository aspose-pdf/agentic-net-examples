using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string htmlPagesFolder = "html_pages";
        const string combinedHtmlPath = "combined.html";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Convert PDF to multiple HTML pages (one per PDF page)
        // ------------------------------------------------------------
        Directory.CreateDirectory(htmlPagesFolder);
        string baseHtmlFile = Path.Combine(htmlPagesFolder, "page.html");

        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // HtmlSaveOptions – split each PDF page into a separate HTML file
            Aspose.Pdf.HtmlSaveOptions htmlOpts = new Aspose.Pdf.HtmlSaveOptions();
            htmlOpts.SplitIntoPages = true;
            // Optional: embed images as PNG inside SVG to keep everything self‑contained
            htmlOpts.RasterImagesSavingMode = Aspose.Pdf.HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;

            pdfDoc.Save(baseHtmlFile, htmlOpts);
        }

        // ------------------------------------------------------------
        // 2. Combine the generated HTML pages into a single HTML file
        // ------------------------------------------------------------
        StringBuilder combinedBuilder = new StringBuilder();

        // Files are named: page.html, page_2.html, page_3.html, ...
        // Sort them numerically to preserve page order
        string[] htmlFiles = Directory.GetFiles(htmlPagesFolder, "page*.html");
        Array.Sort(htmlFiles, (a, b) =>
        {
            int GetNumber(string path)
            {
                string name = Path.GetFileNameWithoutExtension(path); // e.g., "page_2"
                if (name.Equals("page", StringComparison.OrdinalIgnoreCase))
                    return 1; // first page
                int underscore = name.LastIndexOf('_');
                if (underscore >= 0 && int.TryParse(name.Substring(underscore + 1), out int n))
                    return n;
                return int.MaxValue;
            }
            return GetNumber(a).CompareTo(GetNumber(b));
        });

        foreach (string htmlFile in htmlFiles)
        {
            combinedBuilder.AppendLine(File.ReadAllText(htmlFile));
        }

        File.WriteAllText(combinedHtmlPath, combinedBuilder.ToString(), Encoding.UTF8);

        // ------------------------------------------------------------
        // 3. Load the combined HTML and convert it back to PDF
        // ------------------------------------------------------------
        using (Aspose.Pdf.Document htmlDoc = new Aspose.Pdf.Document(combinedHtmlPath, new Aspose.Pdf.HtmlLoadOptions()))
        {
            htmlDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion complete. Output PDF saved to '{outputPdfPath}'.");
    }
}

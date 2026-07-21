using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Ensure an input PDF exists for the demo (sandbox has no files initially)
        CreateSamplePdf(pdfPath);

        // Folder where individual HTML pages and the index will be placed
        const string outputFolder = "HtmlPages";
        Directory.CreateDirectory(outputFolder);

        // Base name for the generated HTML files (Aspose will append "_pageN.html")
        string baseHtmlPath = Path.Combine(outputFolder, "document.html");

        // Configure HTML conversion: one HTML file per PDF page
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            SplitIntoPages = true
        };

        // Convert PDF to HTML (Windows only; wrap in try‑catch for cross‑platform safety)
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                pdfDoc.Save(baseHtmlPath, htmlOptions);
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
            return;
        }

        // After conversion Aspose creates files named "document_page1.html", "document_page2.html", …
        string[] pageFiles = Directory.GetFiles(outputFolder, "document_page*.html");
        Array.Sort(pageFiles); // Ensure pages are in numeric order

        // Build an index.html that links to each page file
        string indexPath = Path.Combine(outputFolder, "index.html");
        using (StreamWriter writer = new StreamWriter(indexPath))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html><head><meta charset=\"UTF-8\"><title>PDF Pages Index</title></head><body>");
            writer.WriteLine("<h1>Index of PDF Pages</h1>");
            writer.WriteLine("<ul>");

            for (int i = 0; i < pageFiles.Length; i++)
            {
                string fileName = Path.GetFileName(pageFiles[i]);
                writer.WriteLine($"<li><a href=\"{fileName}\">Page {i + 1}</a></li>");
            }

            writer.WriteLine("</ul>");
            writer.WriteLine("</body></html>");
        }

        Console.WriteLine($"Conversion completed. Index file created at: {indexPath}");
    }

    // Helper method to create a minimal PDF so the example can run in an empty sandbox
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a single blank page (you could add content if desired)
            doc.Pages.Add();
            doc.Save(path);
        }
    }
}

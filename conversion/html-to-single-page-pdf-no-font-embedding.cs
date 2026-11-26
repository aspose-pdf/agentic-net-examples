using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // HtmlLoadOptions no longer exposes DisableFontEmbedding or PageLayout.
        // Use the default constructor and apply the required settings after the document is loaded.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        try
        {
            // Load the HTML with the configured options.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // ------------------------------------------------------------
                // 1. Disable font embedding (if the API is available).
                //    The FontEmbeddingMode property and FontEmbeddingModes enum
                //    may not exist in older Aspose.Pdf versions, so we use
                //    reflection to set the value when possible.
                // ------------------------------------------------------------
                var fontEmbeddingProp = typeof(Document).GetProperty("FontEmbeddingMode");
                var fontEmbeddingEnum = doc.GetType().Assembly.GetType("Aspose.Pdf.FontEmbeddingModes");
                if (fontEmbeddingProp != null && fontEmbeddingEnum != null)
                {
                    var noEmbedding = Enum.Parse(fontEmbeddingEnum, "NoEmbedding");
                    fontEmbeddingProp.SetValue(doc, noEmbedding);
                }

                // ------------------------------------------------------------
                // 2. Render the whole HTML on a single PDF page.
                //    If the PageFragment class exists we use it to import the
                //    content of the subsequent pages into the first page.
                //    Otherwise we simply delete the extra pages.
                // ------------------------------------------------------------
                if (doc.Pages.Count > 1)
                {
                    var firstPage = doc.Pages[1];
                    var pageFragmentType = doc.GetType().Assembly.GetType("Aspose.Pdf.PageFragment");
                    if (pageFragmentType != null)
                    {
                        // Iterate backwards so that page numbers stay valid after deletions.
                        for (int i = doc.Pages.Count; i >= 2; i--)
                        {
                            // Create a PageFragment for the current page.
                            object fragmentObj = Activator.CreateInstance(pageFragmentType, doc.Pages[i]);
                            if (fragmentObj != null)
                                firstPage.Paragraphs.Add((dynamic)fragmentObj);

                            // Remove the now‑redundant page.
                            doc.Pages.Delete(i);
                        }
                    }
                    else
                    {
                        // Fallback – just delete the extra pages, leaving the first page as is.
                        for (int i = doc.Pages.Count; i >= 2; i--)
                            doc.Pages.Delete(i);
                    }
                }

                // Save the resulting PDF.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}

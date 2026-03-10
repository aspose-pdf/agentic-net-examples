using System;
using System.IO;
using System.Drawing; // Required for PdfContentEditor rectangle and color parameters
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string zugferdXmlPath = "invoice.xml";   // ZUGFeRD XML to embed
        const string svgFolderPath  = "VectorGraphics"; // Folder to store extracted SVGs

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Ensure the folder for SVG output exists
        Directory.CreateDirectory(svgFolderPath);

        // ------------------------------------------------------------
        // 1. Load the PDF document (lifecycle: using block)
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 2. Work with vector graphics
            //    - Detect vector graphics on each page
            //    - If present, export them as SVG files
            // ------------------------------------------------------------
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                if (page.HasVectorGraphics())
                {
                    string svgPath = Path.Combine(svgFolderPath, $"Page_{pageIndex}.svg");
                    bool saved = page.TrySaveVectorGraphics(svgPath);
                    Console.WriteLine(saved
                        ? $"Vector graphics of page {pageIndex} saved to {svgPath}"
                        : $"Failed to save vector graphics of page {pageIndex}");
                }
            }

            // ------------------------------------------------------------
            // 3. Embed ZUGFeRD XML (invoice data) into the PDF
            //    - Use Document.BindXml which attaches the XML as a
            //      file attachment and marks the PDF as ZUGFeRD compliant
            // ------------------------------------------------------------
            pdfDoc.BindXml(zugferdXmlPath);
            Console.WriteLine("ZUGFeRD XML embedded.");

            // ------------------------------------------------------------
            // 4. Add JavaScript actions
            //    - Document level: show an alert when the PDF is opened
            //    - Page level: show an alert when a specific page is opened
            // ------------------------------------------------------------
            pdfDoc.OpenAction = new Aspose.Pdf.Annotations.JavascriptAction("app.alert('Document opened via JavaScript');");
            // Example: add a page open action to the second page (if it exists)
            if (pdfDoc.Pages.Count >= 2)
            {
                Page secondPage = pdfDoc.Pages[2];
                secondPage.Actions.OnOpen = new Aspose.Pdf.Annotations.JavascriptAction("app.alert('Second page opened via JavaScript');");
            }
            Console.WriteLine("JavaScript actions added.");

            // ------------------------------------------------------------
            // 5. Create navigation links using PdfContentEditor
            //    - Create a local link on page 1 that jumps to page 2
            //    - Use System.Drawing.Rectangle for the clickable area
            // ------------------------------------------------------------
            PdfContentEditor contentEditor = new PdfContentEditor();
            contentEditor.BindPdf(pdfDoc);
            // Define a rectangle (x, y, width, height) in points
            System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 700, 200, 50);
            // Create a local link from page 1 to page 2 with a visible red border
            contentEditor.CreateLocalLink(linkRect, 1, 2, System.Drawing.Color.Red);
            contentEditor.Save(outputPdfPath); // Save changes made by the editor
            contentEditor.Close();
            Console.WriteLine("Local navigation link created.");

            // ------------------------------------------------------------
            // 6. Add bookmarks for each page using PdfBookmarkEditor
            //    - This helps compare navigation structures
            // ----------------------------------------------------------
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(outputPdfPath); // Bind to the already saved file to avoid conflicts
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                bookmarkEditor.CreateBookmarkOfPage($"Page {i}", i);
            }
            // Save the document again to include bookmarks
            bookmarkEditor.Save(outputPdfPath);
            bookmarkEditor.Close();
            Console.WriteLine("Bookmarks added for each page.");

            Console.WriteLine($"Processing completed. Output saved to '{outputPdfPath}'.");
        }
    }
}
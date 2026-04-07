using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Recursively builds a Bookmark hierarchy from an XML element.
    static Bookmark BuildBookmark(XElement element)
    {
        // Create a bookmark for the current XML element.
        Bookmark bm = new Bookmark
        {
            Title = element.Name.LocalName,
            Action = "GoTo"
        };

        // Optional: map an XML attribute named "page" to the bookmark's target page.
        if (int.TryParse(element.Attribute("page")?.Value, out int pageNumber))
            bm.PageNumber = pageNumber;

        // If the element has child elements, create a Bookmarks collection for them.
        if (element.HasElements)
        {
            Bookmarks childBookmarks = new Bookmarks();
            foreach (XElement child in element.Elements())
                childBookmarks.Add(BuildBookmark(child));

            bm.ChildItem = childBookmarks;
        }

        return bm;
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // PDF to which bookmarks will be added
        const string xmlPath       = "structure.xml"; // XML describing the hierarchy
        const string outputPdfPath = "output_with_bookmarks.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML that defines the hierarchical structure.
            XDocument xDoc = XDocument.Load(xmlPath);

            // Initialize the bookmark editor and bind the PDF.
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(inputPdfPath);

            // Build and add bookmarks for each top‑level XML element.
            foreach (XElement rootElement in xDoc.Root.Elements())
            {
                Bookmark rootBookmark = BuildBookmark(rootElement);
                editor.CreateBookmarks(rootBookmark);
            }

            // Save the modified PDF.
            editor.Save(outputPdfPath);
            editor.Close();

            Console.WriteLine($"Bookmarks created and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
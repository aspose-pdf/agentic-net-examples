using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xmlPath   = "outline.xml";    // XML defining outline hierarchy
        const string outputPdf = "output.pdf";     // PDF with customized bookmarks

        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Load the XML that describes the bookmark hierarchy.
        XDocument xDoc = XDocument.Load(xmlPath);

        // Initialize the bookmark editor and bind the source PDF.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(pdfPath);

        // The XML root can contain multiple <bookmark> elements.
        foreach (XElement xb in xDoc.Root.Elements("bookmark"))
        {
            Bookmark bm = BuildBookmarkFromXml(xb);
            // Add the (possibly nested) bookmark to the document.
            editor.CreateBookmarks(bm);
        }

        // Save the modified PDF.
        editor.Save(outputPdf);
        editor.Close(); // optional cleanup

        Console.WriteLine($"PDF saved with customized outline: {outputPdf}");
    }

    // Recursively converts an XML <bookmark> element into an Aspose.Pdf.Facades.Bookmark.
    // Expected XML format:
    // <bookmark title="Chapter 1" page="1">
    //     <bookmark title="Section 1.1" page="2"/>
    //     <bookmark title="Section 1.2" page="3"/>
    // </bookmark>
    static Bookmark BuildBookmarkFromXml(XElement element)
    {
        // Required attributes.
        string title = (string)element.Attribute("title") ?? "Untitled";
        int page    = (int?)element.Attribute("page") ?? 1;

        // Create the bookmark and set navigation to the specified page.
        Bookmark bm = new Bookmark
        {
            Title      = title,
            PageNumber = page,
            Action     = "GoTo"
        };

        // Process child <bookmark> elements recursively.
        var childElements = element.Elements("bookmark");
        if (childElements.Any())
        {
            Bookmarks childCollection = new Bookmarks();
            foreach (XElement child in childElements)
            {
                childCollection.Add(BuildBookmarkFromXml(child));
            }
            bm.ChildItem = childCollection;
        }

        return bm;
    }
}
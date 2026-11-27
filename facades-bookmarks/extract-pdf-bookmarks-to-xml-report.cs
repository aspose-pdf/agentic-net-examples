using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // Bookmark and Bookmarks are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "pages_report.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and extract its bookmarks using the facade API
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdf);
            // ExtractBookmarks returns a *Bookmarks* collection, not an array
            Bookmarks rootBookmarks = bookmarkEditor.ExtractBookmarks();

            // Build XML document with bookmark titles and page numbers
            XDocument xmlDoc = new XDocument(new XElement("Bookmarks"));
            ProcessBookmarks(rootBookmarks, xmlDoc.Root);
            xmlDoc.Save(outputXml);

            // Save/close the editor (optional, but good practice)
            bookmarkEditor.Save(inputPdf); // no changes to PDF, just to close the stream
            bookmarkEditor.Close();
        }

        Console.WriteLine($"Bookmark report saved to '{outputXml}'.");
    }

    // Recursively adds bookmarks (and their children) to the XML tree
    static void ProcessBookmarks(Bookmarks bookmarks, XElement parentElement)
    {
        if (bookmarks == null) return;

        foreach (Bookmark bm in bookmarks)
        {
            // In the facade model the page number is exposed via PageNumber
            int pageNumber = bm.PageNumber;

            XElement bookmarkElement = new XElement(
                "Bookmark",
                new XAttribute("Title", bm.Title ?? string.Empty),
                new XAttribute("Page", pageNumber)
            );

            parentElement.Add(bookmarkElement);

            // Child bookmarks are accessed through the ChildItems collection
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                ProcessBookmarks(bm.ChildItems, bookmarkElement);
            }
        }
    }
}

using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "summary.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            try
            {
                // Bind the PDF document to the editor.
                bookmarkEditor.BindPdf(pdfPath);

                // Extract the top‑level bookmark collection using the facade model.
                // The correct return type is Aspose.Pdf.Facades.Bookmarks.
                Bookmarks rootBookmarks = bookmarkEditor.ExtractBookmarks();

                // Create an XML writer for the summary file.
                using (XmlWriter writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Bookmarks");

                    // Recursively write all bookmarks.
                    WriteBookmarks(writer, rootBookmarks);

                    writer.WriteEndElement(); // </Bookmarks>
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Bookmark summary saved to '{xmlPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Recursively writes a collection of Bookmark objects to the XML writer.
    static void WriteBookmarks(XmlWriter writer, Bookmarks collection)
    {
        foreach (Bookmark bm in collection)
        {
            writer.WriteStartElement("Bookmark");
            writer.WriteAttributeString("Title", bm.Title ?? string.Empty);

            // In the facade model the page number is exposed via the PageNumber property.
            if (bm.PageNumber > 0)
                writer.WriteAttributeString("Page", bm.PageNumber.ToString());

            // Process child bookmarks, if any. In the facade model the child collection is called ChildItems.
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
                WriteBookmarks(writer, bm.ChildItems);

            writer.WriteEndElement(); // </Bookmark>
        }
    }
}

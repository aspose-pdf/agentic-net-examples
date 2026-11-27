using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Xml;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "bookmarks.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Bind the PDF to the bookmark editor (facade model)
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(inputPdfPath);

                // Extract the collection of bookmarks – the facade model returns a "Bookmarks" collection
                Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

                // Create an XML file with title and page number for each bookmark
                XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
                using (XmlWriter writer = XmlWriter.Create(outputXmlPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Bookmarks");

                    foreach (Bookmark bm in bookmarks)
                    {
                        writer.WriteStartElement("Bookmark");
                        writer.WriteAttributeString("Title", bm.Title ?? string.Empty);
                        // In the facade model the page number is exposed via the PageNumber property
                        writer.WriteAttributeString("PageNumber", bm.PageNumber.ToString());
                        writer.WriteEndElement(); // Bookmark
                    }

                    writer.WriteEndElement(); // Bookmarks
                    writer.WriteEndDocument();
                }

                // No modifications are made to the PDF, but we close the editor to release resources
                bookmarkEditor.Close();
            }

            Console.WriteLine($"Bookmarks extracted to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string ofdPath   = "input.ofd";   // Source OFD file
        const string outputPdf = "output.pdf";  // Destination PDF file

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load the OFD file as a PDF document using the correct load options
        using (Document pdfDoc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Convert the OFD outlines (bookmarks) into Aspose.Pdf.Bookmark objects
            var rootBookmark = new Bookmark { Title = "Root" };
            foreach (OutlineItemCollection outline in pdfDoc.Outlines)
            {
                rootBookmark.ChildItems.Add(ConvertOutlineItem(outline));
            }

            // Initialize the bookmark editor and bind it to the document
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(pdfDoc);

                // Add the converted bookmark hierarchy to the PDF
                bookmarkEditor.CreateBookmarks(rootBookmark);

                // Save the PDF with the imported bookmarks
                bookmarkEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with imported bookmarks saved to '{outputPdf}'.");
    }

    // Recursively converts an OutlineItemCollection (OFD bookmark) to a Bookmark object
    static Bookmark ConvertOutlineItem(OutlineItemCollection source)
    {
        var dest = new Bookmark
        {
            Title       = source.Title,
            // Destination in OFD outlines is an IAppointment; convert it to string for PDF bookmark
            Destination = source.Destination?.ToString()
            // PageNumber is not available for OFD outlines – it is encoded in the Destination string
        };

        // Recursively copy child outline items, if any
        foreach (OutlineItemCollection child in source) // OutlineItemCollection itself is enumerable over its children
        {
            dest.ChildItems.Add(ConvertOutlineItem(child));
        }
        return dest;
    }
}

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int expectedBookmarks = 3; // set the expected number of bookmarks

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and save it to a new file
        using (Document doc = new Document(inputPath))
        {
            doc.Save(outputPath);
        }

        // Use PdfBookmarkEditor (Facades) to extract bookmarks to an XML file
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(outputPath);

        string tempXml = Path.GetTempFileName();
        try
        {
            bookmarkEditor.ExtractBookmarks(tempXml);

            // Load the exported XML and count <Bookmark> elements
            XDocument xdoc = XDocument.Load(tempXml);
            int actualCount = xdoc.Descendants("Bookmark").Count();

            if (actualCount == expectedBookmarks)
                Console.WriteLine($"Validation succeeded: {actualCount} bookmarks found.");
            else
                Console.WriteLine($"Validation failed: expected {expectedBookmarks}, but found {actualCount}.");
        }
        finally
        {
            // Clean up the temporary XML file
            if (File.Exists(tempXml))
                File.Delete(tempXml);
        }
    }
}
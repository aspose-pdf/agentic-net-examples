using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file, XML bookmarks file and output PDF file paths
        const string inputPdfPath = "input.pdf";
        const string bookmarksXmlPath = "bookmarks.xml";
        const string outputPdfPath = "output.pdf";

        // Verify that the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        if (!File.Exists(bookmarksXmlPath))
        {
            Console.Error.WriteLine($"Error: Bookmarks XML file not found – {bookmarksXmlPath}");
            return;
        }

        try
        {
            // Create the PdfBookmarkEditor facade
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document
                bookmarkEditor.BindPdf(inputPdfPath);

                // Import bookmarks from the XML file
                bookmarkEditor.ImportBookmarksWithXML(bookmarksXmlPath);

                // Save the modified PDF to the output file
                bookmarkEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"Bookmarks imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
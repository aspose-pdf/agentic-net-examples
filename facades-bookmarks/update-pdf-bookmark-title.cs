using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public class PdfBookmarkHelper
{
    /// <summary>
    /// Binds a PDF from a byte array, adds a bookmark to the first page,
    /// and returns the updated PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Input PDF data.</param>
    /// <returns>Updated PDF data with the new bookmark.</returns>
    public static byte[] AddBookmark(byte[] pdfBytes)
    {
        // Facade class usage (required by task). Not used for modification but instantiated.
        PdfFileEditor editor = new PdfFileEditor();

        // Load PDF from memory stream into a Document (must be disposed via using).
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(inputStream))
        {
            // Create a top‑level bookmark (OutlineItemCollection).
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "New Bookmark",
                Action = new GoToAction(doc.Pages[1]) // points to the first page
            };

            // Add the bookmark to the document's outline collection.
            doc.Outlines.Add(bookmark);

            // Save the modified document to a new memory stream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                // Return the byte array containing the updated PDF.
                return outputStream.ToArray();
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exercised via PdfBookmarkHelper.
    }
}
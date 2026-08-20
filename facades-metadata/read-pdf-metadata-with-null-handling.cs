using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Ensure the file exists before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo implements IDisposable; wrap it in a using block for deterministic cleanup
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // ----- Read a custom metadata property -----
            // GetMetaInfo returns an empty string when the property does not exist.
            string customKey = "MyCustomProperty";
            string customValue = pdfInfo.GetMetaInfo(customKey);

            // Gracefully handle null or empty values
            if (string.IsNullOrEmpty(customValue))
            {
                customValue = "(not set)";
            }

            Console.WriteLine($"{customKey}: {customValue}");

            // ----- Read standard metadata properties (e.g., Title) -----
            // These properties are exposed directly on PdfFileInfo.
            string title = pdfInfo.Title;
            if (string.IsNullOrEmpty(title))
            {
                title = "(no title)";
            }
            Console.WriteLine($"Title: {title}");

            // Example for Author property
            string author = pdfInfo.Author;
            if (string.IsNullOrEmpty(author))
            {
                author = "(no author)";
            }
            Console.WriteLine($"Author: {author}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Collection of HTML strings to be converted into a single PDF.
        List<string> htmlStrings = new List<string>
        {
            "<html><body><h1>First Document</h1><p>Hello World!</p></body></html>",
            "<html><body><h2>Second Document</h2><p>Another paragraph.</p></body></html>"
            // Add more HTML strings as needed.
        };

        // Path for the resulting PDF file.
        const string outputPdfPath = "CombinedOutput.pdf";

        // List to hold temporary Document objects created from each HTML string.
        List<Document> tempDocs = new List<Document>();

        // Load each HTML string into a separate Document using HtmlLoadOptions.
        foreach (string html in htmlStrings)
        {
            // Convert the HTML string to a UTF-8 memory stream.
            using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
            {
                // Configure custom rendering options for HTML loading.
                HtmlLoadOptions loadOptions = new HtmlLoadOptions
                {
                    // Embed fonts into the resulting PDF.
                    IsEmbedFonts = true,
                    // Render each HTML page as a separate PDF page (default behavior).
                    IsRenderToSinglePage = false
                    // Additional options can be set here as needed.
                };

                // Load the HTML content into a Document.
                Document doc = new Document(htmlStream, loadOptions);
                tempDocs.Add(doc);
            }
        }

        // Ensure there is at least one document to work with.
        if (tempDocs.Count == 0)
        {
            Console.Error.WriteLine("No HTML content provided.");
            return;
        }

        // Use the first document as the target and merge the rest into it.
        using (Document targetDoc = tempDocs[0])
        {
            // Merge remaining documents into the target document.
            for (int i = 1; i < tempDocs.Count; i++)
            {
                using (Document srcDoc = tempDocs[i])
                {
                    // The Merge method appends pages from srcDoc to targetDoc.
                    targetDoc.Merge(srcDoc);
                }
            }

            // Save the combined PDF to the specified file.
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created successfully at '{outputPdfPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXps = "output.xps";
        const string searchPhrase = "hello world";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Create absorber to search for the specified phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

                // Perform search on the whole document
                absorber.Visit(doc);

                // Output found fragments
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"Found \"{fragment.Text}\" on page {fragment.Page.Number}");
                }

                // Save the document as XPS using explicit save options
                XpsSaveOptions xpsOptions = new XpsSaveOptions();
                doc.Save(outputXps, xpsOptions);
            }

            Console.WriteLine($"Search completed. XPS saved to '{outputXps}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
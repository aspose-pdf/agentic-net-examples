using System;
using System.IO;
using Aspose.Pdf; // MdLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        const string markdownPath = "input.md";
        const string pdfPath      = "output.pdf";

        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"Markdown file not found: {markdownPath}");
            return;
        }

        try
        {
            // Configure loading options. PreserveCodeBlocks is enabled by default, so no explicit property is required.
            MdLoadOptions loadOptions = new MdLoadOptions();

            // Load the Markdown file into a Document using the options
            using (Document doc = new Document(markdownPath, loadOptions))
            {
                // Save the document as PDF (default format, no SaveOptions needed)
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Markdown converted to PDF successfully: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

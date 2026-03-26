using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string templatePath = "template.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        try
        {
            using (Document targetDoc = new Document(inputPath))
            using (Document templateDoc = new Document(templatePath))
            {
                // The first page of the template contains the background image.
                Page templatePage = templateDoc.Pages[1];
                PdfPageStamp pageStamp = new PdfPageStamp(templatePage);
                pageStamp.Background = true; // stamp as background

                // Apply the stamp to every page of the target document.
                foreach (Page page in targetDoc.Pages)
                {
                    pageStamp.Put(page);
                }

                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Background image applied to all pages. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
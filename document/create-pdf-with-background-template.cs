using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "output.pdf";
        const int  pageCount     = 5; // number of pages to generate

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Load the PDF page that will be used as background template
        using (Document templateDoc = new Document(templatePath))
        {
            // Create the target document
            using (Document outputDoc = new Document())
            {
                // Add blank pages to the target document
                for (int i = 1; i <= pageCount; i++)
                {
                    outputDoc.Pages.Add();
                }

                // Create a stamp from the first page of the template
                PdfPageStamp backgroundStamp = new PdfPageStamp(templateDoc.Pages[1]);
                backgroundStamp.Background = true; // place stamp behind page content

                // Apply the background stamp to every page of the target document
                for (int i = 1; i <= outputDoc.Pages.Count; i++)
                {
                    outputDoc.Pages[i].AddStamp(backgroundStamp);
                }

                // Save the resulting PDF
                outputDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with background template saved to '{outputPath}'.");
    }
}
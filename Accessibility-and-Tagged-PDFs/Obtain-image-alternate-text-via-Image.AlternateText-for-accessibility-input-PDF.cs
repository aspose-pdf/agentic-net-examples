using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path
        const string inputPath = "input.pdf";

        // Verify the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate over each page in the document
            foreach (Page page in pdfDocument.Pages)
            {
                // Iterate over each image resource on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve alternate text for the image on this page
                    IList<string> altTexts = img.GetAlternativeText(page);

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        foreach (string txt in altTexts)
                        {
                            Console.WriteLine($"Page {page.Number}, Image \"{img.Name}\": Alt Text = \"{txt}\"");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Page {page.Number}, Image \"{img.Name}\": No alternate text.");
                    }
                }
            }

            // Save the (unchanged) document to a new file
            const string outputPath = "output.pdf";
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
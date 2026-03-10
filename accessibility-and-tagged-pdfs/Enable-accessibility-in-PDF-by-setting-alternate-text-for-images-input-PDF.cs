using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_images.pdf";
        const string altText    = "Accessible image description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate through each page and each image on the page
                foreach (Page page in doc.Pages)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Set alternative text for the image.
                        // The method returns false if the image cannot be uniquely identified.
                        bool result = img.TrySetAlternativeText(altText, page);
                        // Optional: handle the case where setting the alt text fails.
                        // if (!result) { /* log or take action */ }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with alternate text: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
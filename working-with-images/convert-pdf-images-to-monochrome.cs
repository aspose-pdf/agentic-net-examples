using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf       = "input.pdf";
        const string outputPdf      = "output_monochrome.pdf";
        const string monoImagePath  = "mono.png"; // monochrome image used for replacement

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(monoImagePath))
        {
            Console.Error.WriteLine($"Monochrome image not found: {monoImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over each page
            foreach (Page page in doc.Pages)
            {
                // Copy current images to a list to avoid modifying the collection while iterating
                var images = page.Resources.Images.Cast<XImage>().ToList();

                foreach (XImage img in images)
                {
                    // Remove the original image from the page resources
                    page.Resources.Images.Remove(img);

                    // Add the monochrome image in its place
                    using (FileStream monoStream = File.OpenRead(monoImagePath))
                    {
                        page.Resources.Images.Add(monoStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with monochrome images to '{outputPdf}'.");
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect the PDF file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-pdf>");
            return;
        }

        string inputPath = args[0];

        // Verify that the file exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document.
        Document pdfDocument = new Document(inputPath);

        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            Page page = pdfDocument.Pages[pageNumber];

            // Access the collection of images on the current page.
            var images = page.Resources.Images;
            if (images == null) continue;

            // For each image, retrieve its alternate text (if any).
            foreach (XImage image in images)
            {
                List<string> altTexts = image.GetAlternativeText(page);
                string alt = (altTexts != null && altTexts.Count > 0)
                    ? string.Join("; ", altTexts)
                    : "(none)";

                Console.WriteLine($"Page {pageNumber}, Image Name: {image.Name}, Alt Text: {alt}");
            }
        }
    }
}
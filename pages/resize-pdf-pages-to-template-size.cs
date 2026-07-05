using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for PageSize class

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";   // reference PDF with desired page size
        const string inputPath    = "input.pdf";      // PDF to be resized
        const string outputPath   = "resized_output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Load the template and obtain its first page dimensions
        PageSize targetSize;
        using (Document templateDoc = new Document(templatePath))
        {
            Page templatePage = templateDoc.Pages[1]; // pages are 1‑based
            float width  = (float)(templatePage.Rect.URX - templatePage.Rect.LLX);
            float height = (float)(templatePage.Rect.URY - templatePage.Rect.LLY);
            targetSize = new PageSize(width, height);
        }

        // Load the PDF to be resized and apply the target size to each page
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.Resize(targetSize);
            }

            // Save the resized document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
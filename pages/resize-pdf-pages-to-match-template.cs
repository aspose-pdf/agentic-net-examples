using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string inputPath    = "input.pdf";
        const string outputPath   = "output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the template PDF to obtain its page dimensions
        using (Document templateDoc = new Document(templatePath))
        // Load the PDF whose pages need to be resized
        using (Document inputDoc = new Document(inputPath))
        {
            // Assume all template pages share the same size; use the first page as reference
            double templateWidth  = templateDoc.Pages[1].PageInfo.Width;
            double templateHeight = templateDoc.Pages[1].PageInfo.Height;

            // Resize every page in the input document to match the template dimensions
            for (int i = 1; i <= inputDoc.Pages.Count; i++)
            {
                Page page = inputDoc.Pages[i];
                page.SetPageSize(templateWidth, templateHeight);
            }

            // Save the modified document
            inputDoc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to match '{templatePath}' and saved as '{outputPath}'.");
    }
}
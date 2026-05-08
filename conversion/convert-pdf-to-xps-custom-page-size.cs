using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xps";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define custom page size (example: 8.5 x 11 inches).
        // 1 inch = 72 points.
        double widthInches = 8.5;
        double heightInches = 11;
        double width = widthInches * 72;
        double height = heightInches * 72;

        // Set orientation: true for landscape, false for portrait.
        bool landscape = true;
        if (landscape)
        {
            // Swap dimensions for landscape orientation.
            double temp = width;
            width = height;
            height = temp;
        }

        // Load the PDF, apply custom page size/orientation, and save as XPS.
        using (Document doc = new Document(inputPath))
        {
            // Apply the custom size to every page using PageInfo.
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width = width;
                page.PageInfo.Height = height;
                page.PageInfo.IsLandscape = landscape;
            }

            // Initialize XPS save options.
            XpsSaveOptions saveOptions = new XpsSaveOptions();

            // Save the document as XPS using the specified options.
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputPath}");
    }
}

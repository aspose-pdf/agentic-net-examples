using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Load the background PDF (template) and create the target PDF
        using (Document templateDoc = new Document(templatePath))
        using (Document resultDoc   = new Document())
        {
            // Number of pages to generate using the same background
            int pageCount = 5;

            // The first page of the template will be used as a background.
            // For each required page we clone that template page into the result document.
            for (int i = 0; i < pageCount; i++)
            {
                // Clone the template page (page 1) into the result document.
                // This copies all contents of the template page, effectively using it as a background.
                Page newPage = resultDoc.Pages.Add(templateDoc.Pages[1]);

                // If you need to add additional content on top of the background, do it here.
                // Example: add a simple text fragment indicating the page number.
                // TextFragment tf = new TextFragment($"Page {i + 1}")
                // {
                //     TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Color.Black }
                // };
                // newPage.Paragraphs.Add(tf);
            }

            // Save the final PDF
            resultDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with background template: {outputPath}");
    }
}

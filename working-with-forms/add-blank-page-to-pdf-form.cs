using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_with_blank_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF form (fields are loaded automatically)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document.
            // This does not affect any existing form fields.
            Page blankPage = doc.Pages.Add();

            // Optionally, you could set the page size to match existing pages:
            // blankPage.PageInfo.Width = doc.Pages[1].PageInfo.Width;
            // blankPage.PageInfo.Height = doc.Pages[1].PageInfo.Height;

            // Save the modified document, preserving all form fields.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added successfully. Saved to '{outputPath}'.");
    }
}
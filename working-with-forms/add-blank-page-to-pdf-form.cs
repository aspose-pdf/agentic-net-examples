using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

namespace AsposePdfApi
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "input_form.pdf";   // Existing PDF form
            const string outputPath = "output_with_blank_page.pdf";

            // Verify input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Load the existing PDF document (form) inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Add a new blank page at the end of the document.
                // The Add() overload creates an empty page using the most common page size.
                // Existing form fields remain intact because we are only modifying the page collection.
                Page newPage = doc.Pages.Add();

                // Optionally, set page size or other properties on the new page.
                // For example, match the size of the first page:
                // newPage.PageInfo = doc.Pages[1].PageInfo;

                // Save the updated document. No SaveOptions are needed because we are saving as PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Blank page added. Saved to '{outputPath}'.");
        }
    }
}

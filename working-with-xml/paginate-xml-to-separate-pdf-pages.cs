using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input XML file that will be converted to PDF.
        const string xmlPath = "large_dataset.xml";

        // Directory where the paginated PDF files will be saved.
        const string outputDir = "PaginatedPdfs";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the XML and convert it to a PDF document.
        // XmlLoadOptions is required for XML input.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document sourceDoc = new Document(xmlPath, loadOptions))
        {
            int totalPages = sourceDoc.Pages.Count;

            // Iterate over each page and create a separate PDF file.
            for (int pageIndex = 1; pageIndex <= totalPages; pageIndex++)
            {
                // Create a new empty document for the single page.
                using (Document singlePageDoc = new Document())
                {
                    // Copy the current page from the source document.
                    // The Add method clones the page into the new document.
                    singlePageDoc.Pages.Add(sourceDoc.Pages[pageIndex]);

                    // Optional: add a footer with the sequential page number.
                    Page page = singlePageDoc.Pages[1];
                    TextFragment footer = new TextFragment($"Page {pageIndex}");
                    footer.TextState.FontSize = 12;
                    footer.TextState.Font = FontRepository.FindFont("Helvetica");
                    footer.HorizontalAlignment = HorizontalAlignment.Center;
                    footer.VerticalAlignment = VerticalAlignment.Bottom;
                    page.Paragraphs.Add(footer);

                    // Build the output file name with sequential numbering.
                    string outputPath = Path.Combine(outputDir, $"Document_Part_{pageIndex}.pdf");

                    // Save the single‑page PDF.
                    singlePageDoc.Save(outputPath);
                    Console.WriteLine($"Saved page {pageIndex} to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine("Pagination complete.");
    }
}
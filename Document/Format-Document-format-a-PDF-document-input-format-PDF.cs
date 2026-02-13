using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Apply formatting to each page
            foreach (Page page in pdfDocument.Pages)
            {
                // Set page size to A4
                page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

                // Define uniform margins (20 points on each side)
                page.PageInfo.Margin = new MarginInfo { Top = 20, Bottom = 20, Left = 20, Right = 20 };

                // Ensure no rotation
                page.Rotate = Rotation.None;
            }

            // Add a centered header to every page
            foreach (Page page in pdfDocument.Pages)
            {
                TextFragment header = new TextFragment("Sample Header")
                {
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                // Configure TextState (TextState is read‑only, modify its members instead of assigning a new instance)
                header.TextState.FontSize = 12;
                header.TextState.Font = FontRepository.FindFont("Arial");
                header.TextState.ForegroundColor = Color.Gray;

                // Position the header just below the top margin
                header.Position = new Position(0, page.PageInfo.Margin.Top - 10);
                page.Paragraphs.Add(header);
            }

            // Add a centered footer with page numbers
            int pageCount = pdfDocument.Pages.Count;
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = pdfDocument.Pages[i];
                TextFragment footer = new TextFragment($"Page {i} of {pageCount}")
                {
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                // Configure TextState
                footer.TextState.FontSize = 10;
                footer.TextState.Font = FontRepository.FindFont("Arial");
                footer.TextState.ForegroundColor = Color.Gray;

                // Position the footer just above the bottom margin
                footer.Position = new Position(0, page.PageInfo.Margin.Bottom - 5);
                page.Paragraphs.Add(footer);
            }

            // Save the formatted PDF document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF successfully saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}
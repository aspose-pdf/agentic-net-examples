using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Define output PDF path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output.pdf");

        try
        {
            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Create a new PDF document using the Form facade
            using (Form pdfForm = new Form())
            {
                // Access the underlying Document object
                Document doc = pdfForm.Document;

                // Add a blank page
                Page page = doc.Pages.Add();

                // Create a simple text fragment
                TextFragment tf = new TextFragment("Hello, Aspose.Pdf Facades!")
                {
                    // Set basic text properties
                    TextState = { FontSize = 24, Font = FontRepository.FindFont("Arial"), ForegroundColor = Color.Blue }
                };

                // Add the text fragment to the page
                page.Paragraphs.Add(tf);

                // Save the PDF using the facade's Save method
                pdfForm.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating PDF: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Bind the document to a PdfPageEditor facade (used for saving)
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(pdfDocument);

                // Create a new blank page and add it to the document
                Page newPage = pdfDocument.Pages.Add();

                // Optional: add some content to the new page
                TextFragment tf = new TextFragment("This is a newly added page.");
                tf.TextState.FontSize = 14;
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.ForegroundColor = Color.Black;
                newPage.Paragraphs.Add(tf);

                // Save the modified PDF using the facade
                pageEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF successfully updated. New page added and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
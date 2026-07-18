using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // needed for HorizontalAlignment & VerticalAlignment

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure an input PDF exists for the sandbox. If it does not, create a
        // minimal document, add a single blank page and save it to the expected
        // location. This follows the "hardcoded-input-file-generate-inline-first"
        // rule required for the execution environment.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPath);
            }
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Use the document's creation date if available; otherwise fall back to current date
        DateTime creationDate = pdfDocument.Info?.CreationDate ?? DateTime.Now;
        string dateText = creationDate.ToString("yyyy-MM-dd");

        // Create a TextStamp for the date
        TextStamp dateStamp = new TextStamp(dateText)
        {
            TextState = { Font = FontRepository.FindFont("Helvetica"), FontSize = 12, ForegroundColor = Color.Black },
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            XIndent = 20f, // left margin
            YIndent = 20f  // top margin
        };

        // Add the stamp to every page (or just the first page if preferred)
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(dateStamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
    }
}

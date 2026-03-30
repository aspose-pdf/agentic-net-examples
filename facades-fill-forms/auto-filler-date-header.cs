using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Load the template PDF
        using (Document doc = new Document(templatePath))
        {
            // Add a header with the current date on every page of the document
            string dateString = DateTime.Now.ToString("d"); // e.g., 03/30/2026
            foreach (Page page in doc.Pages)
            {
                TextFragment header = new TextFragment(dateString);
                header.TextState.FontSize = 12;
                header.TextState.Font = FontRepository.FindFont("Helvetica");
                // Position near the top of the page (50 units from left, 20 units from top edge)
                header.Position = new Position(50, page.PageInfo.Height - 20);
                page.Paragraphs.Add(header);
            }

            // If you need to fill form fields, use the Form class (AutoFiller does not exist in Aspose.Pdf)
            // Example (optional):
            // Form form = new Form(doc);
            // form.FillForm(...);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Generated PDF with date header saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.docx";

        // Create a new PDF document (will be saved as Word)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Add a text fragment to the page
            TextFragment text = new TextFragment("Hello, this is a Word document created via Aspose.Pdf.");
            page.Paragraphs.Add(text);

            // Configure save options for Word format (DOCX)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document as a Word file
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"Word document saved to '{outputPath}'.");
    }
}
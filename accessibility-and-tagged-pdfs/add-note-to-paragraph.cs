using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment that represents the main paragraph
            TextFragment paragraph = new TextFragment("This is the main paragraph.");

            // Attach a note (footnote) to the paragraph for supplemental information
            paragraph.FootNote = new Note("Supplemental information provided as a footnote.");

            // Add the paragraph (with its footnote) to the page
            page.Paragraphs.Add(paragraph);

            // Save the PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
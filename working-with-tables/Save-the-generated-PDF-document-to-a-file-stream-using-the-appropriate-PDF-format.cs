using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page and a simple text fragment
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello, Aspose.Pdf!"));

            // Save the document to a file stream (PDF format is implicit)
            using (FileStream outputStream = new FileStream("output.pdf", FileMode.Create, FileAccess.Write))
            {
                doc.Save(outputStream);
            }
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Try to load an existing PDF; if it does not exist, create a new one in memory.
        Document doc;
        const string inputPath = "input.pdf";
        if (File.Exists(inputPath))
        {
            // Load the PDF bytes into a memory stream and open the document from the stream.
            byte[] pdfBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            {
                doc = new Document(inputStream);
            }
        }
        else
        {
            // Create a new PDF with a single blank page.
            doc = new Document();
            doc.Pages.Add();
        }

        // Ensure the document has at least one page.
        if (doc.Pages.Count > 0)
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content.
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");

            // Position the text on the page (coordinates are in points).
            tf.Position = new Position(100, 700);

            // Set font, size, and color.
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the text fragment to the page using TextBuilder.
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);
        }

        // Save the modified PDF into an output memory stream.
        using (MemoryStream outputStream = new MemoryStream())
        {
            doc.Save(outputStream);
            // Example: write the result to a file (optional).
            File.WriteAllBytes("output.pdf", outputStream.ToArray());
        }
    }
}

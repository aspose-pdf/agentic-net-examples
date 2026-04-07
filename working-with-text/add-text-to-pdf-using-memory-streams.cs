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
        if (File.Exists("input.pdf"))
        {
            // Load PDF bytes into a memory stream (you could also use FileStream directly).
            byte[] pdfBytes = File.ReadAllBytes("input.pdf");
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            {
                doc = new Document(inputStream);
            }
        }
        else
        {
            // No source PDF – create a blank document with a single page.
            doc = new Document();
            doc.Pages.Add();
        }

        // Ensure the document is disposed after use.
        using (doc)
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content.
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
            // Set position on the page (coordinates are in points).
            tf.Position = new Position(100, 700);
            // Configure visual appearance.
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Blue;

            // Append the text fragment to the page using TextBuilder.
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified document to an output memory stream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                // Reset stream position if it will be read afterwards.
                outputStream.Position = 0;

                // Example: write the result to a file (optional).
                File.WriteAllBytes("output.pdf", outputStream.ToArray());
            }
        }
    }
}

using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Create a TextAbsorber (rule: use TextAbsorber for extraction)
                TextAbsorber absorber = new TextAbsorber();

                // Extract text from the first page (pages are 1‑based)
                doc.Pages[1].Accept(absorber);

                // Get the extracted text (may be empty)
                string extracted = absorber.Text ?? string.Empty;

                // Write the text to a UTF‑8 encoded file
                File.WriteAllText(outputTxt, extracted, Encoding.UTF8);
            }

            Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
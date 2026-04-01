using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string password = "user123"; // replace with actual user password if needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document. Supplying a password works for both encrypted and non‑encrypted PDFs.
        using (Document doc = new Document(inputPath, password))
        {
            // If the PDF is encrypted, decrypt it.
            if (doc.IsEncrypted)
            {
                doc.Decrypt();
            }

            // Modify the PDF – add a simple text fragment on the first page.
            TextFragment text = new TextFragment("Modified by Aspose.Pdf");
            text.TextState.FontSize = 12;
            text.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            text.Position = new Position(100, 700);

            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(text);

            // Save the updated PDF.
            doc.Save(outputPath);
            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
    }
}
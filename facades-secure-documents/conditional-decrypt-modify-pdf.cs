using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Added for PdfFileInfo

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string password = "userPassword"; // password for encrypted PDFs (if any)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Determine whether the PDF is encrypted using the Facades namespace.
            PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
            bool isEncrypted = fileInfo.IsEncrypted;

            // Load the PDF using Document – it supports a password overload.
            Document pdfDoc = isEncrypted
                ? new Document(inputPath, password)
                : new Document(inputPath);

            // Example modification: add a text string at position (100, 500) on the first page.
            TextFragment tf = new TextFragment("Document modified by Aspose.Pdf");
            tf.Position = new Position(100, 500);
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Color.Black;

            // Add the fragment to the first page (pages are 1‑based).
            pdfDoc.Pages[1].Paragraphs.Add(tf);

            // Save the modified PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

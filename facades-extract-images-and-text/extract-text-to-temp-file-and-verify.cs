using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and extract its text
        string extractedText;
        using (Document doc = new Document(inputPdfPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            // Accept the absorber for all pages (pages are 1‑based)
            doc.Pages.Accept(absorber);
            extractedText = absorber.Text;
        }

        // Save the extracted text to a temporary file
        string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".txt");
        File.WriteAllText(tempFilePath, extractedText);

        // Read the text back from the temporary file
        string readBackText = File.ReadAllText(tempFilePath);

        // Verify that the saved and read text match the original extraction
        if (extractedText == readBackText)
        {
            Console.WriteLine("Verification succeeded: extracted text matches the saved content.");
        }
        else
        {
            Console.WriteLine("Verification failed: mismatch between extracted and saved text.");
        }

        // Clean up the temporary file
        try
        {
            File.Delete(tempFilePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}
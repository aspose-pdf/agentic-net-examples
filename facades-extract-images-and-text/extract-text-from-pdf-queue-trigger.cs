using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    // Simulated "queue" and "blob" storage using local folders.
    //   inbox  : folder that contains PDF files (acts as the PDF blob container).
    //   outbox : folder where extracted text files are written (acts as the text blob container).
    static void Main()
    {
        const string inboxFolder = "inbox";          // stands in for the PDF blob container
        const string outboxFolder = "extracted-text"; // stands in for the text blob container

        Directory.CreateDirectory(inboxFolder);
        Directory.CreateDirectory(outboxFolder);

        // Process each PDF file found in the inbox folder.
        foreach (string pdfPath in Directory.GetFiles(inboxFolder, "*.pdf"))
        {
            string pdfFileName = Path.GetFileName(pdfPath);
            try
            {
                // Extract text using Aspose.Pdf.Facades.PdfExtractor.
                using (var extractor = new PdfExtractor())
                {
                    // Bind the PDF file directly (no need for a MemoryStream).
                    extractor.BindPdf(pdfPath);

                    // Extract text – specify UTF‑8 encoding to ensure proper characters.
                    extractor.ExtractText(Encoding.UTF8);

                    // Retrieve the extracted text via a stream because GetText() has no
                    // parameter‑less overload. The stream will contain the UTF‑8 bytes.
                    string extractedText;
                    using (var textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        extractedText = Encoding.UTF8.GetString(textStream.ToArray());
                    }

                    // Write the extracted text to a .txt file with the same base name.
                    string txtFileName = Path.ChangeExtension(pdfFileName, ".txt");
                    string txtPath = Path.Combine(outboxFolder, txtFileName);
                    File.WriteAllText(txtPath, extractedText, Encoding.UTF8);

                    Console.WriteLine($"[INFO] Extracted text from '{pdfFileName}' and saved to '{txtFileName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to process '{pdfFileName}': {ex.Message}");
            }
        }

        Console.WriteLine("[INFO] Processing complete.");
    }
}

using System;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfAuditProcessor
{
    // Simple audit logger – appends a line to a log file.
    private static void LogOperation(string operation, int pageNumber, string detail)
    {
        string timestamp = DateTime.UtcNow.ToString("o"); // ISO 8601 format
        string logLine = $"{timestamp} | {operation} | Page: {pageNumber} | Detail: {detail}";
        File.AppendAllText("audit.log", logLine + Environment.NewLine);
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // ----- Add Image using PdfFileMend -----
        const string imagePath = "logo.png";
        int imagePage = 1;
        float imgLlX = 50f, imgLlY = 700f, imgUrX = 150f, imgUrY = 800f;

        // Ensure input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load PDF and bind it to PdfFileMend
        using (Document doc = new Document(inputPdfPath))
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(doc); // initialize facade with the document

            // Add the image
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                bool added = mend.AddImage(imgStream, imagePage, imgLlX, imgLlY, imgUrX, imgUrY);
                if (added)
                {
                    LogOperation("AddImage", imagePage, Path.GetFileName(imagePath));
                }
                else
                {
                    Console.Error.WriteLine("Failed to add image.");
                }
            }

            // Save intermediate result (still the final output file)
            mend.Save(outputPdfPath);
        }

        // ----- Add Text using PdfContentEditor -----
        const string textContent = "Confidential";
        int textPage = 2;
        // System.Drawing.Rectangle expects (x, y, width, height)
        System.Drawing.Rectangle textRect = new System.Drawing.Rectangle(
            100,               // X (lower‑left X)
            500,               // Y (lower‑left Y)
            300 - 100,         // Width
            550 - 500);        // Height

        const string fontName = "Helvetica";
        const int fontSize = 24;
        const bool isBold = false;
        const string colorHex = "#FF0000"; // Red in hex

        // Load the PDF (the one just saved with the image) and bind it to PdfContentEditor
        using (Document doc = new Document(outputPdfPath))
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(doc); // initialize facade with the document

            // Create a text annotation on the specified page
            editor.CreateText(textRect, textContent, fontName, isBold, colorHex, fontSize);
            LogOperation("AddText", textPage, textContent);

            // Save final PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdfPath}'. Audit log written to 'audit.log'.");
    }
}
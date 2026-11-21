using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string auditLogPath  = "audit.log";

        // Example data: images and texts to add
        var imagesToAdd = new (string ImagePath, int PageNumber)[]
        {
            ("logo.png", 1),
            ("chart.jpg", 2)
        };

        var textsToAdd = new (string Text, int PageNumber, double X, double Y)[]
        {
            ("Confidential", 1, 100, 700),
            ("Draft", 2, 150, 650)
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure audit log file exists
        using (var logWriter = new StreamWriter(auditLogPath, append: true))
        {
            try
            {
                using (Document pdfDoc = new Document(inputPdfPath))
                {
                    // Add images
                    foreach (var (imagePath, pageNumber) in imagesToAdd)
                    {
                        if (!File.Exists(imagePath))
                        {
                            Console.Error.WriteLine($"Image not found: {imagePath}");
                            continue;
                        }

                        // Create an ImageStamp and add it to the specified page
                        ImageStamp imgStamp = new ImageStamp(imagePath)
                        {
                            // Example positioning – adjust as needed
                            XIndent = 50,
                            YIndent = 50,
                            // Keep original size
                            Width  = 0,
                            Height = 0,
                            // Place on top of page content
                            Background = false
                        };

                        // Pages are 1‑based
                        pdfDoc.Pages[pageNumber].AddStamp(imgStamp);

                        // Log the operation
                        string logEntry = $"{DateTime.UtcNow:u} | AddImage | Page {pageNumber} | File \"{Path.GetFileName(imagePath)}\"";
                        logWriter.WriteLine(logEntry);
                        Console.WriteLine(logEntry);
                    }

                    // Add texts
                    foreach (var (text, pageNumber, x, y) in textsToAdd)
                    {
                        // Create a TextFragment with the desired content
                        TextFragment tf = new TextFragment(text)
                        {
                            Position = new Position(x, y)
                        };

                        // Optional styling
                        tf.TextState.Font = FontRepository.FindFont("Helvetica");
                        tf.TextState.FontSize = 12;
                        tf.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

                        // Add to the specified page
                        pdfDoc.Pages[pageNumber].Paragraphs.Add(tf);

                        // Log the operation
                        string logEntry = $"{DateTime.UtcNow:u} | AddText | Page {pageNumber} | Text \"{text}\"";
                        logWriter.WriteLine(logEntry);
                        Console.WriteLine(logEntry);
                    }

                    // Save the modified PDF
                    pdfDoc.Save(outputPdfPath);
                }

                Console.WriteLine($"PDF saved to \"{outputPdfPath}\". Audit log written to \"{auditLogPath}\".");
            }
            catch (Exception ex)
            {
                string err = $"Error: {ex.Message}";
                logWriter.WriteLine($"{DateTime.UtcNow:u} | ERROR | {err}");
                Console.Error.WriteLine(err);
            }
        }
    }
}
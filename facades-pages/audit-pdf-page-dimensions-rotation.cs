using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logFilePath = "audit_log.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Log page dimensions and rotation BEFORE any edits
        // -----------------------------------------------------------------
        using (Document srcDoc = new Document(inputPdfPath))
        using (StreamWriter logWriter = new StreamWriter(logFilePath, false))
        {
            logWriter.WriteLine("=== BEFORE EDITS ===");
            for (int i = 1; i <= srcDoc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = srcDoc.Pages[i];
                double width = page.PageInfo.Width;
                double height = page.PageInfo.Height;
                int rotation = (int)page.Rotate; // Rotation enum -> int (0,90,180,270)

                logWriter.WriteLine($"Page {i}: Width={width:F2}, Height={height:F2}, Rotation={rotation}");
            }
        }

        // -----------------------------------------------------------------
        // 2. Perform edits – rotate each page 90° clockwise
        // -----------------------------------------------------------------
        // Using Document API (PdfPageEditor no longer provides RotatePage)
        using (Document doc = new Document(inputPdfPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Rotate = Rotation.on90; // rotate 90° clockwise
            }
            doc.Save(outputPdfPath);
        }

        // -----------------------------------------------------------------
        // 3. Log page dimensions and rotation AFTER edits
        // -----------------------------------------------------------------
        using (Document editedDoc = new Document(outputPdfPath))
        using (StreamWriter logWriter = new StreamWriter(logFilePath, true)) // append
        {
            logWriter.WriteLine();
            logWriter.WriteLine("=== AFTER EDITS ===");
            for (int i = 1; i <= editedDoc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = editedDoc.Pages[i];
                double width = page.PageInfo.Width;
                double height = page.PageInfo.Height;
                int rotation = (int)page.Rotate;

                logWriter.WriteLine($"Page {i}: Width={width:F2}, Height={height:F2}, Rotation={rotation}");
            }
        }

        Console.WriteLine($"Audit log written to '{logFilePath}'. Edited PDF saved as '{outputPdfPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "edited.pdf";
        const string auditLogPath = "audit_log.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open audit log for writing
        using (StreamWriter log = new StreamWriter(auditLogPath, false))
        {
            // ---------- BEFORE EDIT ----------
            using (Document docBefore = new Document(inputPdfPath))
            {
                log.WriteLine("=== BEFORE EDIT ===");
                for (int i = 1; i <= docBefore.Pages.Count; i++)
                {
                    Page page = docBefore.Pages[i];
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;
                    Rotation rotation = page.Rotate;
                    log.WriteLine($"Page {i}: Width={width}, Height={height}, Rotation={rotation}");
                }
            }

            // ---------- EDIT ----------
            // Add a simple text stamp to each page using the Document API (PdfPageEditor.InsertText no longer exists)
            using (Document doc = new Document(inputPdfPath))
            {
                // Prepare a font for the inserted text
                Font font = FontRepository.FindFont("Arial");
                Aspose.Pdf.Color textColor = Aspose.Pdf.Color.Black;

                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    TextFragment tf = new TextFragment("Edited by Aspose.Pdf.Facades");
                    tf.Position = new Position(50, 50); // X, Y coordinates (points from bottom‑left)
                    tf.TextState.Font = font;
                    tf.TextState.FontSize = 12;
                    tf.TextState.ForegroundColor = textColor;
                    doc.Pages[i].Paragraphs.Add(tf);
                }

                doc.Save(outputPdfPath);
            }

            // ---------- AFTER EDIT ----------
            using (Document docAfter = new Document(outputPdfPath))
            {
                log.WriteLine("=== AFTER EDIT ===");
                for (int i = 1; i <= docAfter.Pages.Count; i++)
                {
                    Page page = docAfter.Pages[i];
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;
                    Rotation rotation = page.Rotate;
                    log.WriteLine($"Page {i}: Width={width}, Height={height}, Rotation={rotation}");
                }
            }
        }

        Console.WriteLine($"Audit log written to '{auditLogPath}'. Edited PDF saved as '{outputPdfPath}'.");
    }
}

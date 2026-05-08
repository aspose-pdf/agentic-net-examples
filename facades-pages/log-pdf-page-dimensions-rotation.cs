using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "edited.pdf";
        const string logPath   = "audit_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1 – Gather initial page dimensions and rotation values
        // -----------------------------------------------------------------
        StringWriter beforeLog = new StringWriter();

        using (PdfFileInfo info = new PdfFileInfo(inputPdf))
        {
            int pageCount = info.NumberOfPages;
            beforeLog.WriteLine("=== BEFORE EDIT ===");
            for (int i = 1; i <= pageCount; i++)
            {
                double width  = info.GetPageWidth(i);
                double height = info.GetPageHeight(i);
                int    rot    = info.GetPageRotation(i);
                beforeLog.WriteLine($"Page {i}: Width={width:F2} pt, Height={height:F2} pt, Rotation={rot}°");
            }
        }

        // -----------------------------------------------------------------
        // Step 2 – Apply an edit (rotate all pages by 90 degrees)
        // -----------------------------------------------------------------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPdf);

            // Rotate all pages 90 degrees clockwise
            editor.Rotation = 90;          // valid values: 0, 90, 180, 270
            editor.ApplyChanges();

            // Save the edited PDF
            editor.Save(outputPdf);
        }

        // -----------------------------------------------------------------
        // Step 3 – Gather post‑edit page dimensions and rotation values
        // -----------------------------------------------------------------
        StringWriter afterLog = new StringWriter();

        using (PdfFileInfo info = new PdfFileInfo(outputPdf))
        {
            int pageCount = info.NumberOfPages;
            afterLog.WriteLine("=== AFTER EDIT ===");
            for (int i = 1; i <= pageCount; i++)
            {
                double width  = info.GetPageWidth(i);
                double height = info.GetPageHeight(i);
                int    rot    = info.GetPageRotation(i);
                afterLog.WriteLine($"Page {i}: Width={width:F2} pt, Height={height:F2} pt, Rotation={rot}°");
            }
        }

        // -----------------------------------------------------------------
        // Step 4 – Write audit information to a text file
        // -----------------------------------------------------------------
        try
        {
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                writer.WriteLine($"Audit Log – {DateTime.UtcNow:u}");
                writer.WriteLine();
                writer.Write(beforeLog.ToString());
                writer.WriteLine();
                writer.Write(afterLog.ToString());
            }

            Console.WriteLine($"Audit log written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write audit log: {ex.Message}");
        }
    }
}
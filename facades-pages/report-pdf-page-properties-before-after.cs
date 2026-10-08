using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "edited.pdf";
        const string reportPath = "page_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);
        int pageCount = pdfDoc.Pages.Count; // 1‑based indexing

        // Prepare the report
        using (StreamWriter report = new StreamWriter(reportPath, false))
        {
            report.WriteLine("Page Properties Report");
            report.WriteLine($"Source: {inputPath}");
            report.WriteLine($"Edited: {outputPath}");
            report.WriteLine(new string('=', 40));

            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];

                // ----- BEFORE EDIT -----
                double beforeWidth = page.PageInfo.Width;
                double beforeHeight = page.PageInfo.Height;
                int beforeRotation = (int)page.Rotate; // Rotation enum values are 0,90,180,270
                double beforeZoom = 1.0; // default zoom (no native property)

                // ----- CALCULATE AFTER VALUES -----
                double afterWidth = beforeWidth * 1.10;
                double afterHeight = beforeHeight * 1.10;
                int afterRotationDeg = (beforeRotation + 90) % 360;
                double afterZoom = beforeZoom * 1.50;

                // Apply size changes
                page.PageInfo.Width = afterWidth;
                page.PageInfo.Height = afterHeight;

                // Apply rotation change using the Page.Rotate property
                page.Rotate = (Rotation)afterRotationDeg;

                // ----- AFTER EDIT (values read back from the page) -----
                double finalWidth = page.PageInfo.Width;
                double finalHeight = page.PageInfo.Height;
                int finalRotation = (int)page.Rotate;
                double finalZoom = afterZoom; // stored value, not a native attribute

                // Write details to the report
                report.WriteLine($"Page {pageNumber}:");
                report.WriteLine("  Before Edit:");
                report.WriteLine($"    Size     : {beforeWidth:F2} x {beforeHeight:F2}");
                report.WriteLine($"    Rotation : {beforeRotation}°");
                report.WriteLine($"    Zoom     : {beforeZoom:F2}");
                report.WriteLine("  After Edit:");
                report.WriteLine($"    Size     : {finalWidth:F2} x {finalHeight:F2}");
                report.WriteLine($"    Rotation : {finalRotation}°");
                report.WriteLine($"    Zoom     : {finalZoom:F2}");
                report.WriteLine(new string('-', 30));
            }
        }

        // Apply zoom to all pages using PdfPageEditor (native support)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(pdfDoc);
            // Process all pages (1‑based array)
            int[] allPages = new int[pageCount];
            for (int i = 0; i < pageCount; i++) allPages[i] = i + 1;
            editor.ProcessPages = allPages;
            editor.Zoom = 1.5f; // 150% magnification (matches the calculated afterZoom)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Editing completed. Report saved to '{reportPath}'. Edited PDF saved to '{outputPath}'.");
    }
}

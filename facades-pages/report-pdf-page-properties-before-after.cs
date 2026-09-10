using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Report page properties BEFORE any edits
        // -----------------------------------------------------------------
        Console.WriteLine("=== BEFORE EDIT ===");
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            int pageCount = info.NumberOfPages;
            for (int i = 1; i <= pageCount; i++) // 1‑based indexing
            {
                double width = info.GetPageWidth(i);
                double height = info.GetPageHeight(i);
                int rotation = info.GetPageRotation(i);
                double xOffset = info.GetPageXOffset(i);
                double yOffset = info.GetPageYOffset(i);

                Console.WriteLine(
                    $"Page {i}: Size=({width} x {height}), Rotation={rotation}°, XOffset={xOffset}, YOffset={yOffset}");
            }
        }

        // -----------------------------------------------------------------
        // Edit pages: rotate first page, apply a zoom factor, and shift origin
        // -----------------------------------------------------------------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Rotate page 1 by 90 degrees (use Dictionary<int,int>)
            editor.PageRotations = new Dictionary<int, int> { { 1, 90 } };

            // Apply a zoom of 80% to all pages (float literal)
            editor.Zoom = 0.8f;

            // Move the content origin by (10, 20) points
            editor.MovePosition(10, 20);

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        // -----------------------------------------------------------------
        // Report page properties AFTER the edits
        // -----------------------------------------------------------------
        Console.WriteLine("\n=== AFTER EDIT ===");
        using (PdfFileInfo info = new PdfFileInfo(outputPath))
        {
            int pageCount = info.NumberOfPages;
            for (int i = 1; i <= pageCount; i++) // 1‑based indexing
            {
                double width = info.GetPageWidth(i);
                double height = info.GetPageHeight(i);
                int rotation = info.GetPageRotation(i);
                double xOffset = info.GetPageXOffset(i);
                double yOffset = info.GetPageYOffset(i);

                Console.WriteLine(
                    $"Page {i}: Size=({width} x {height}), Rotation={rotation}°, XOffset={xOffset}, YOffset={yOffset}");
            }
        }
    }
}

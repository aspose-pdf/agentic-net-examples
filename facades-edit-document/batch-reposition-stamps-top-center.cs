using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchStampReposition
{
    static void Main()
    {
        // Input PDF files (can be populated as needed)
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Output directory for the processed PDFs
        string outputDir = "RepositionedStamps";

        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Derive output file name
            string outputPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(inputPath) + "_repositioned.pdf");

            // Load the PDF document (lifecycle: using block ensures disposal)
            using (Document doc = new Document(inputPath))
            {
                // Bind the document to PdfContentEditor to manipulate stamps
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Retrieve all stamps on the current page
                    StampInfo[] stamps = editor.GetStamps(pageNum);
                    if (stamps == null || stamps.Length == 0)
                        continue; // No stamps on this page

                    // Page dimensions
                    double pageWidth = doc.Pages[pageNum].PageInfo.Width;
                    double pageHeight = doc.Pages[pageNum].PageInfo.Height;

                    for (int i = 0; i < stamps.Length; i++)
                    {
                        StampInfo stampInfo = stamps[i];

                        // ----- Retrieve stamp width & height via reflection (properties may not exist in older versions) -----
                        double stampWidth = 0;
                        double stampHeight = 0;
                        Type siType = stampInfo.GetType();
                        PropertyInfo widthProp = siType.GetProperty("Width");
                        PropertyInfo heightProp = siType.GetProperty("Height");
                        if (widthProp != null && widthProp.CanRead)
                            stampWidth = Convert.ToDouble(widthProp.GetValue(stampInfo));
                        if (heightProp != null && heightProp.CanRead)
                            stampHeight = Convert.ToDouble(heightProp.GetValue(stampInfo));

                        // Calculate new X coordinate (centered horizontally). If width is unknown, fall back to page centre.
                        double newX = (stampWidth > 0) ? (pageWidth - stampWidth) / 2.0 : pageWidth / 2.0;
                        // Calculate new Y coordinate (top of the page). If height is unknown, place at page top.
                        double newY = (stampHeight > 0) ? pageHeight - stampHeight : pageHeight;

                        // Move the stamp to the new position. The stamp index is the array position (i).
                        editor.MoveStamp(pageNum, i, newX, newY);
                    }
                }

                // Save the modified document (lifecycle: save inside using block)
                doc.Save(outputPath);
                Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
            }
        }
    }
}

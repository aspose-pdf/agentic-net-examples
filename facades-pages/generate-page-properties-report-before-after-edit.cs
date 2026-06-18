using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Capture original page properties
        var originalProps = new List<PageInfo>();
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            int pageCount = info.NumberOfPages;
            for (int i = 1; i <= pageCount; i++)
            {
                originalProps.Add(new PageInfo
                {
                    PageNumber = i,
                    Width      = info.GetPageWidth(i),
                    Height     = info.GetPageHeight(i),
                    Rotation   = info.GetPageRotation(i)
                });
            }
        }

        // Edit pages: rotate 90°, set zoom to 0.5 (50%)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.Rotation = 90;          // rotate all pages
            editor.Zoom     = 0.5f;        // 50% zoom – float literal required
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        // Capture edited page properties
        var editedProps = new List<PageInfo>();
        using (PdfFileInfo info = new PdfFileInfo(outputPath))
        {
            int pageCount = info.NumberOfPages;
            for (int i = 1; i <= pageCount; i++)
            {
                editedProps.Add(new PageInfo
                {
                    PageNumber = i,
                    Width      = info.GetPageWidth(i),
                    Height     = info.GetPageHeight(i),
                    Rotation   = info.GetPageRotation(i)
                });
            }
        }

        // Output comparison report
        Console.WriteLine("Page Properties Report");
        Console.WriteLine("----------------------");
        Console.WriteLine("{0,5} | {1,12} | {2,12} | {3,8} || {4,12} | {5,12} | {6,8} | {7,6}",
                          "Page", "Orig Width", "Orig Height", "Orig Rot",
                          "New Width", "New Height", "New Rot", "Zoom");
        Console.WriteLine(new string('-', 85));

        for (int i = 0; i < originalProps.Count; i++)
        {
            var o = originalProps[i];
            var n = editedProps[i];
            Console.WriteLine("{0,5} | {1,12:F2} | {2,12:F2} | {3,8} || {4,12:F2} | {5,12:F2} | {6,8} | {7,6}",
                              o.PageNumber,
                              o.Width, o.Height, o.Rotation,
                              n.Width, n.Height, n.Rotation,
                              "0.5");
        }
    }

    class PageInfo
    {
        public int    PageNumber { get; set; }
        public double Width      { get; set; }
        public double Height     { get; set; }
        public int    Rotation   { get; set; }
    }
}

using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        // Desired uniform margins (in points)
        const float leftMargin = 50f;
        const float rightMargin = 50f;
        const float topMargin = 50f;
        const float bottomMargin = 50f;

        using (Document target = new Document(firstPdf))
        using (Document source = new Document(secondPdf))
        {
            // Apply uniform margins to all pages in the first document
            foreach (Page page in target.Pages)
            {
                page.PageInfo.Margin = new MarginInfo(leftMargin, rightMargin, topMargin, bottomMargin);
            }

            // Apply uniform margins to all pages in the second document
            foreach (Page page in source.Pages)
            {
                page.PageInfo.Margin = new MarginInfo(leftMargin, rightMargin, topMargin, bottomMargin);
            }

            // Merge the second document into the first
            target.Pages.Add(source.Pages);

            // Save the merged PDF
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}